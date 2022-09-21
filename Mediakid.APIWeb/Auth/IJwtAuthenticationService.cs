using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using MediaKid.EntidadesDeNegocio;

namespace MediaKid.WebAPI.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario pUsuario);
    }
}


