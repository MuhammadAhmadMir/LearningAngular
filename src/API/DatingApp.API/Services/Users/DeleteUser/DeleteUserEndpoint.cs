namespace DatingApp.API.Services.Users.DeleteUser
{
    public record DeleteUserResponse(bool IsSuccess);

    public class DeleteUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id}", async (int Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteUserCommand(Id));
                var response = result.Adapt<DeleteUserResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteAppUser")
                .Produces<DeleteUserResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete App User")
                .WithDescription("Delete App User");
        }
    }
}
