

using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        string Authenticate(AuthenticationRequest authenticationRequest);
    }
}
