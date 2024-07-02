
namespace DatingApp.API.Services.Users
{
    [ScopedService]
    public class UserDataProvider(IDataProviderBase<AppUser> dataProvider)
    {
        public async Task<IEnumerable<AppUser>> GetAllUsersAsync() => await dataProvider.GetAllAsync();

        public async Task<AppUser> GetUserByIdAsync(int id) => await dataProvider.GetByIdAsync(id);

        public async Task AddUserAsync(AppUser entity)
        {
            await dataProvider.AddAsync(entity);
            await dataProvider.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await dataProvider.GetByIdAsync(id);
            if (user != null)
            {
                dataProvider.Delete(user);
                await dataProvider.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(AppUser entity)
        {
            dataProvider.Update(entity);
            await dataProvider.SaveChangesAsync();
        }
    }
}
