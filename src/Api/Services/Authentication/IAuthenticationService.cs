namespace Api.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(string email, string password);
    }
}