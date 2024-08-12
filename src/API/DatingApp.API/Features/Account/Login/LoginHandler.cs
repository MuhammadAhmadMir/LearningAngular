
using DatingApp.API.Interfaces;

namespace DatingApp.API.Services.Account.Login
{
    public record LoginQuery(string Username, string Password) : IQuery<LoginResult>;

    public record LoginResult(string Username, string Password, string Token);


    public class LoginHandler(IAccountRepository _accountRepository) : IQueryHandler<LoginQuery, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var loginRequestDto = new LoginRequestDto(query.Username, query.Password);

            var result = await _accountRepository.LoginAsync(loginRequestDto);

            return new LoginResult(result.Username, result.Email, result.Token);
        }
    }
}
