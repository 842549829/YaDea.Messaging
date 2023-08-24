namespace YaDea.Messaging.Identity.Services
{
    public interface IUserService
    {
        Task<TokenResult> RegisterAsync(string username, string password, string address);

        Task<TokenResult> LoginAsync(string username, string password);

        Task<TokenResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
