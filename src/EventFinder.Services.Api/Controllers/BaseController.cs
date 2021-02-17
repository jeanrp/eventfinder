﻿using EventFinder.Domain.Core.Notifications;
using EventFinder.Domain.Interfaces;
using EventFinder.Infra.CrossCutting.IoC.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EventFinder.Services.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;
        protected Guid UsuarioLogadoId { get; set; }

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                            IUser user,
                            IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;

            if (user.IsAuthenticated())
            {
                UsuarioLogadoId = user.GetUserId();
            }
        }

        protected new IActionResult Response(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }


        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }


        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        }


        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

    }
}
