
using FirebaseAdmin.Auth;

namespace Api.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<string> RegisterAsync(string email, string password)
        {
            var userArgs = new UserRecordArgs
            {
                Email = email,
                Password = password
            };

            var usuario = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);
            
            return usuario.Uid;
        }
    }
}