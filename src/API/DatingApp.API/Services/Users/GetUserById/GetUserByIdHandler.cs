
namespace DatingApp.API.Services.Users.GetUserById
{
    public record GetUserByIdQuery(int Id) : IQuery<GetUserByIdResult>;
    public record GetUserByIdResult(AppUser User);

    public class GetUserByIdHandler(UserDataProvider userProvider) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await userProvider.GetUserByIdAsync(query.Id);

            return new GetUserByIdResult(user);
        }
    }
}
