namespace DatingApp.API.Services.Users.GetUsers
{
    //public record GetUsersRequest();

    public record GetUsersResponse(IEnumerable<AppUser> Users);

    public class GetUsersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users", async (ISender sender) =>
            {
                var result = await sender.Send(new GetUsersQuery());
                var response = result.Adapt<GetUsersResponse>();

                return Results.Ok(response);
            })
                .WithName("GetAppUsers")
                .Produces<GetUsersResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All App Users")
                .WithDescription("Get All App Users");
        }
    }
}
