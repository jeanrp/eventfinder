﻿using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class ClienteAdicionadoEvent : BaseClienteEvent
    {
        public ClienteAdicionadoEvent(Guid id, string nome, string email, string senha, string telefone, string facebook, DateTime dataNascimento, string sexo, string estadoCivil, string atracaoPreferida, string estiloPreferido)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Telefone = telefone;
            Facebook = facebook;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            AtracaoPreferida = atracaoPreferida;
            EstiloPreferido = estiloPreferido;            
            AggregateId = Id;
        }
    }
}
