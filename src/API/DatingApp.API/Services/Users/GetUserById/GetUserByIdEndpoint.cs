
namespace DatingApp.API.Services.Users.GetUserById
{
    public record GetUserByIdResponse(AppUser User);

    public class GetUserByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id}", async (int Id, ISender sender) =>
            {
                var result = await sender.Send(new GetUserByIdQuery(Id));
                var response = result.Adapt<GetUserByIdResponse>();

                return Results.Ok(response);
            })
                .WithName("GetUserById")
                .Produces<GetUserByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get User By Id")
                .WithDescription("Get User By Id");
        }
    }
}
