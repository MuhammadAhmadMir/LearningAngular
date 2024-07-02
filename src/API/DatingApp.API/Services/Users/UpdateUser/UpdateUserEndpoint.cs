
namespace DatingApp.API.Services.Users.UpdateUser
{
    public record UpdateUserRequest(int Id, string UserName);
    public record UpdateUserResponse(bool IsSuccess);

    public class UpdateUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/users", async (UpdateUserRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateUserCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateUserResponse>();

                return Results.Ok(response);
            })
                .WithName("UpdateAppUser")
                .Produces<UpdateUserResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update App User")
                .WithDescription("Update App User");
        }
    }
}
