namespace DatingApp.API.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CreateUserAccountAsync(RegisterAccountDto dto);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
