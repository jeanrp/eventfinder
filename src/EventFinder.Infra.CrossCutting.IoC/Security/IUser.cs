using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EventFinder.Infra.CrossCutting.IoC.Security
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
