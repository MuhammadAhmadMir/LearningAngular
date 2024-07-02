
namespace DatingApp.API.Services.Users.DeleteUser
{
    public record DeleteUserCommand(int Id) : ICommand<DeleteUserResult>;
    public record DeleteUserResult(bool IsSuccess);


    public class DeleteUserHandler(UserDataProvider userProvider) : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            await userProvider.DeleteUserAsync(command.Id);

            return new DeleteUserResult(true);
        }
    }
}
