
namespace DatingApp.API.Services.Account.RegisterAccount
{
    public record RegisterAccountRequest(string Username, string Password);

    public record RegisterAccountResponse(bool IsSuccess);

    public class RegisterAccountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/account/register", async (RegisterAccountRequest request, ISender sender) =>
            {
                var command = request.Adapt<RegisterAccountCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<RegisterAccountResponse>();

                return Results.Created("/account/login", response);
            }).AllowAnonymous()
            .WithName("RegisterAccount")
            .Produces<RegisterAccountResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register Account")
            .WithDescription("Register Account");
        }
    }
}
