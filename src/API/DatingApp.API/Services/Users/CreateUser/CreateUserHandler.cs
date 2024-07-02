
namespace DatingApp.API.Services.Users.CreateUser
{
    public record CreateUserCommand(string UserName) : ICommand<CreateUserResult>;
    public record CreateUserResult(int Id);

    internal class CreateUserHandler(UserDataProvider userProvider) : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var appUser = new AppUser
            {
                UserName = command.UserName
            };

            await userProvider.AddUserAsync(appUser);

            return new CreateUserResult(appUser.Id);
        }
    }
}
