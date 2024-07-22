using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationServiceOptions _options;
        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password)) 
            {
                return null;
            }

            var user = _userRepository.GetUserByEmail(authenticationRequest.Email);

            if (user == null) 
            {
                return null;
            }

            
            if (user.Password == authenticationRequest.Password)
            {
                return user;
            }
            
            return null;
        }

        public string Authenticate(AuthenticationRequest authenticationRequest) 
        {
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                throw new NotAllowedException("User authentication failed");
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); 
            claimsForToken.Add(new Claim("given_name", user.Name));
            claimsForToken.Add(new Claim("role", user.UserType));

            var jwtSecurityToken = new JwtSecurityToken(
              _options.Issuer,
              _options.Audience,
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
