using Monetrix.Models;

namespace Monetrix.IRepository
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetAppUserByIdAsync(string id);
        Task<IEnumerable<AppUser>> GetAllAppUsersAsync(string? searchString);
        Task CreateAppUserAsync(AppUser appUser);
        Task UpdateAppUserAsync(AppUser appUser);
        Task DeleteAppUserAsync(string id);
    }
}
