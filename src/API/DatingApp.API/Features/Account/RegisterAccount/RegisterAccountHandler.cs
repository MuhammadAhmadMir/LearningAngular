using DatingApp.API.Interfaces;

namespace DatingApp.API.Services.Account.RegisterAccount
{
    public record RegisterAccountCommand(string Username, string Password) : ICommand<RegisterAccountResult>;

    public record RegisterAccountResult(bool IsSuccess);

    public class RegisterAccountHandler(IAccountRepository _accountRepository) : ICommandHandler<RegisterAccountCommand, RegisterAccountResult>
    {
        public async Task<RegisterAccountResult> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
        {

            var registerDto = command.Adapt<RegisterAccountDto>();
            await _accountRepository.CreateUserAccountAsync(registerDto);

            return new RegisterAccountResult(true);
        }
    }
}
