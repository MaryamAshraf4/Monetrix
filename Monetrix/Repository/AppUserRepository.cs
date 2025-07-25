﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monetrix.IRepository;
using Monetrix.Models;
using Monetrix.ViewModels;
using System.Linq;

namespace Monetrix.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetAppUserByIdAsync(string id)
        {
            return await _context.AppUsers.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<AppUser>> GetAllAppUsersAsync(string? searchString)
        {
            var query = _context.AppUsers.AsQueryable();
            if(!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(u => u.FullName.Contains(searchString) || u.Email.Contains(searchString));
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllArchivedUserAsync(string? searchString) {
            var query = _context.AppUsers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(u => u.FullName.Contains(searchString) || u.Email.Contains(searchString));
            }
            return await query.IgnoreQueryFilters().Where(u => u.IsActive == false).ToListAsync();
        }

        public async Task UpdateAppUserAsync(AppUser appUser)
        {
            var oldUser = await GetAppUserByIdAsync(appUser.Id);

            oldUser.FullName = appUser.FullName;
            oldUser.NationalId = appUser.NationalId;
            oldUser.Position = appUser.Position;
            oldUser.Salary = appUser.Salary;
            oldUser.HireDate = appUser.HireDate;
            oldUser.Email = appUser.Email;

            _context.AppUsers.Update(oldUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppUserAsync(string id)
        {
            var appUser = await GetAppUserByIdAsync(id);
            if (appUser != null)
            {
                appUser.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
