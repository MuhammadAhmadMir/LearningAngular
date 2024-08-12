namespace DatingApp.API.DTOs
{
    public record LoginRequestDto(string Username, string Password);

    public record LoginResponseDto(string Username, string Email, string Token);
}
