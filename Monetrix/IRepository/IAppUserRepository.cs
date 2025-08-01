﻿using Monetrix.Models;
using Monetrix.ViewModels;

namespace Monetrix.IRepository
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetAppUserByIdAsync(string id);
        Task<IEnumerable<AppUser>> GetAllAppUsersAsync(string? searchString);
        Task<IEnumerable<AppUser>> GetAllArchivedUserAsync(string? searchString);
        Task UpdateAppUserAsync(AppUser appUser);
        Task DeleteAppUserAsync(string id);
    }
}
