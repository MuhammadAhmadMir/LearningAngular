
namespace DatingApp.API.Services.Users.GetUsers
{
    public record GetUsersQuery() : IQuery<GetUsersResult>;
    public record GetUsersResult(IEnumerable<AppUser> Users);

    public class GetUsersHandler(UserDataProvider usersProvider) : IQueryHandler<GetUsersQuery, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await usersProvider.GetAllUsersAsync();
            
            return new GetUsersResult(users);
        }
    }
}
