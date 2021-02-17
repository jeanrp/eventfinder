using EventFinder.Domain.Usuarios.Repository;
using EventFinder.Domain.Core.Notifications;
using EventFinder.Domain.Interfaces;
using MediatR;
using EventFinder.Domain.Empresas.Repository;
using EventFinder.Domain.Clientes.Repository;
using EventFinder.Domain.Usuarios.Authorization;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using EventFinder.Services.Api.ViewModels;
using AutoMapper;
using EventFinder.Domain.Empresas.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using EventFinder.Domain.Usuarios;
using System.Security.Claims;
using EventFinder.Infra.CrossCutting.IoC.Security;
using System.Security.Principal;
using EventFinder.Domain.Clientes.Commands;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace EventFinder.Services.Api.Controllers
{
    public class AutenticacaoController : BaseController
    {

        private readonly IEmpresaRepository _empresaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler _mediator;
        private readonly JwtTokenOptions _jwtTokenOptions;
        public IConfiguration Configuration { get; set; }

        public AutenticacaoController(
            IConfiguration config,
            IUsuarioRepository usuarioRepository,
            IEmpresaRepository empresaRepository,
            IClienteRepository clienteRepository,
            IOptions<JwtTokenOptions> jwtTokenOptions,
            INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMediatorHandler mediator) : base(notifications, user, mediator)
        {
            Configuration = config;
            _usuarioRepository = usuarioRepository;
            _empresaRepository = empresaRepository;
            _jwtTokenOptions = jwtTokenOptions.Value;
            _clienteRepository = clienteRepository;
            _mediator = mediator;
            ThrowIfInvalidOptions(_jwtTokenOptions);
        }


        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtTokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtTokenOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtTokenOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtTokenOptions.JtiGenerator));
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("nova-conta")]
        public async Task<IActionResult> Register([FromBody]EmpresaViewModel model, int version)
        {
            if (version == 2)
            {
                return Response(new { Message = "API V2 não disponível" });
            }

            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            var empresa = Mapper.Map<IncluirEmpresaCommand>(model);

            await _mediator.EnviarComando(empresa);

            if (!OperacaoValida())
                return Response(model);

            var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Senha = model.Senha });
            return Response(response);
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("nova-conta-cliente")]
        public async Task<IActionResult> Register([FromBody]ClienteViewModel model, int version)
        {
            if (version == 2)
            {
                return Response(new { Message = "API V2 não disponível" });
            }

            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            var cliente = Mapper.Map<AdicionarClienteCommand>(model);

            await _mediator.EnviarComando(cliente);

            if (!OperacaoValida())
                return Response(model);

            var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Senha = model.Senha });
            return Response(response);
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("conta")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            Usuario usuario = _usuarioRepository.BuscarPorEmailSenha(model.Email, model.Senha);

            if (usuario != null)
            {
                var response = GerarTokenUsuario(model);
                return Response(response);
            }

            NotificarErro(usuario.Id.ToString(), "Falha ao realizar o login");
            return Response(model);
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("conta-facebook")]
        public async Task<IActionResult> LoginFacebookEmpresa([FromBody]TokenViewModel tokenVm)
        {
            var accessToken = tokenVm.Token;

            if (string.IsNullOrEmpty(accessToken))
            {
                return Response(accessToken);
            }

            var client = new HttpClient();
            var verifyTokenEndPoint = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name", accessToken);
            var verifyAppEndpoint = string.Format("https://graph.facebook.com/app?access_token={0}", accessToken);
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic userObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                uri = new Uri(verifyAppEndpoint);
                response = await client.GetAsync(uri);
                content = await response.Content.ReadAsStringAsync();
                dynamic appObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                if (appObj["id"] == Configuration["facebook:appid"])
                {
                    string email = string.Empty;
                    if (userObj["email"] != null)
                        email = userObj["email"].ToString();
                    else
                    {
                       var nome = userObj["name"].ToString().Replace(" ", "").ToLower();
                        email = string.Format("{0}@facebook.com", nome);
                    }

                    Usuario usuario = _usuarioRepository.BuscarPorEmailSenha(email, "facebook-login");

                    if (usuario != null)
                    {
                        var resp = GerarTokenUsuario(new LoginViewModel { Email = usuario.Email, Senha = "facebook-login" });
                        return Response(resp);
                    }
                    else
                    {
                        EmpresaViewModel empresaVm = new EmpresaViewModel
                        {
                            Id = Guid.NewGuid(),
                            Email = email,
                            Cnpj = "00000000000000",
                            RazaoSocial = userObj["name"],
                            Senha = "facebook-login",
                            Telefone = "27999999999",
                            UsuarioId = Guid.NewGuid()
                        };

                        var empresa = Mapper.Map<IncluirEmpresaCommand>(empresaVm);
                        await _mediator.EnviarComando(empresa);
                        var resp = GerarTokenUsuario(new LoginViewModel { Email = empresaVm.Email, Senha = empresaVm.Senha });
                        return Response(resp);
                    }
                }
            }

            return Response(new { Message = "Usuário inválido" });
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("conta-facebook-cliente")]
        public async Task<IActionResult> LoginFacebookCliente([FromBody]TokenViewModel tokenVm)
        {
            var accessToken = tokenVm.Token;

            if (string.IsNullOrEmpty(accessToken))
            {
                return Response(accessToken);
            }

            var client = new HttpClient();
            var verifyTokenEndPoint = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name", accessToken);
            var verifyAppEndpoint = string.Format("https://graph.facebook.com/app?access_token={0}", accessToken);
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic userObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                uri = new Uri(verifyAppEndpoint);
                response = await client.GetAsync(uri);
                content = await response.Content.ReadAsStringAsync();
                dynamic appObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                if (appObj["id"] == Configuration["facebook:appid"])
                {
                    var email = userObj["email"].ToString();
                    Usuario usuario = _usuarioRepository.BuscarPorEmailSenha(email, "facebook-login");

                    if (usuario != null)
                    {
                        var resp = GerarTokenUsuario(new LoginViewModel { Email = usuario.Email, Senha = "facebook-login" });
                        return Response(resp);
                    }
                    else
                    {
                        ClienteViewModel clienteVm = new ClienteViewModel
                        {
                            Id = Guid.NewGuid(),
                            Email = userObj["email"],                            
                            Nome = userObj["name"],
                            Senha = "facebook-login",
                            Telefone = "27999999999",
                            DataNascimento = DateTime.Now,
                            EstadoCivil = "I",
                            Sexo = "I",
                            UsuarioId = Guid.NewGuid()
                        };

                        var cliente = Mapper.Map<AdicionarClienteCommand>(clienteVm);
                        await _mediator.EnviarComando(cliente);
                        var resp = GerarTokenUsuario(new LoginViewModel { Email = clienteVm.Email, Senha = clienteVm.Senha });
                        return Response(resp);
                    }
                }
            }

            return Response(new { Message = "Usuário inválido" });
        }

        private async Task<object> GerarTokenUsuario(LoginViewModel login)
        {
            Usuario usuario = _usuarioRepository.BuscarPorEmailSenha(login.Email, login.Senha);

            var identity = await GetClaims(usuario);
            if (identity == null)
            {
                await _mediator.PublicarEvento(new DomainNotification("Identity", "Usuário não encontrado."));
                return Response();
            }

            var claims = new[]
              {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NomeRazaoSocial),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Empresa != null ? usuario.EmpresaId.ToString() : usuario.ClienteId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtTokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtTokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("EventFinder")
            };



            var jwt = new JwtSecurityToken(
                  issuer: _jwtTokenOptions.Issuer,
                  audience: _jwtTokenOptions.Audience,
                  claims: claims.AsEnumerable(),
                  notBefore: _jwtTokenOptions.NotBefore,
                  expires: _jwtTokenOptions.Expiration,
                  signingCredentials: _jwtTokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            if (usuario.EmpresaId != null)
            {
                var empresa = _empresaRepository.BuscarPorEmail(login.Email);

                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)_jwtTokenOptions.ValidFor.TotalSeconds,
                    user = new
                    {
                        id = empresa.Id,
                        nome = empresa.RazaoSocial,
                        email = empresa.Email,
                        claims = claims.Select(x => new { x.Type, x.Value })
                    }
                };
                return response;

            }
            else
            {

                var cliente = _clienteRepository.ObterPorId(usuario.ClienteId.Value);

                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)_jwtTokenOptions.ValidFor.TotalSeconds,
                    user = new
                    {
                        id = cliente.Id,
                        nome = cliente.Nome,
                        email = cliente.Email,
                        claims = claims.Select(x => new { x.Type, x.Value })
                    }
                };
                return response;
            }


        }



        private Task<ClaimsIdentity> GetClaims(Usuario usuario)
        {
            if (usuario == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if (usuario.EmpresaId != null)
            {
                return Task.FromResult(new ClaimsIdentity(
                    new GenericIdentity(usuario.EmpresaId.ToString(), "Token"),
                    new[] {
                    new Claim("EventFinder", "Empresa")
                    }));
            }
            else
            {
                return Task.FromResult(new ClaimsIdentity(
              new GenericIdentity(usuario.ClienteId.ToString(), "Token"),
              new[] {
                    new Claim("EventFinder", "Cliente")
              }));
            }
        }
    }
}

