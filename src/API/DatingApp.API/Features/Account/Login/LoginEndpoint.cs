
using DatingApp.API.Services.Account.RegisterAccount;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Services.Account.Login
{
    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/account/login", async (LoginRequestDto request, ISender sender) =>
            {
                var query = request.Adapt<LoginQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<LoginResponseDto>();

                return Results.Ok(response);
            }).AllowAnonymous()
            .WithName("LoginAccount")
            .Produces<LoginResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Login Account")
            .WithDescription("Login Account");
        }
    }
}
