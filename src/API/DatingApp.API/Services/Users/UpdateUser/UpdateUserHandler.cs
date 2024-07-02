
namespace DatingApp.API.Services.Users.UpdateUser
{
    public record UpdateUserCommand(int Id, string UserName) : ICommand<UpdateUserResult>;

    public record UpdateUserResult(bool IsSuccess);

    public class UpdateUserHandler(UserDataProvider dataProvider) : ICommandHandler<UpdateUserCommand, UpdateUserResult>
    {
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Id = command.Id,
                UserName = command.UserName
            };

            await dataProvider.UpdateAsync(user);

            return new UpdateUserResult(true);
        }
    }
}
