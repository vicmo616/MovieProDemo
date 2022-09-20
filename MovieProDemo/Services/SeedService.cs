﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieProDemo.Data;
using MovieProDemo.Models.Database;
using MovieProDemo.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProDemo.Services
{
    public class SeedService
    {
        private readonly AppSettings _appSettings;

        private readonly ApplicationDbContext _dbContext; 

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedService(IOptions<AppSettings> appSettings, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task ManageDataAsync()
        {
            await UpdateDatabaseAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedCollections();
        }

        private async Task UpdateDatabaseAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (_dbContext.Roles.Any()) return;

            var adminRole = _appSettings.MovieProSettings.DefaultCredentials.Role;

            await _roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        private async Task SeedUsersAsync()
        {
            if (_userManager.Users.Any()) return;

            var credentials = _appSettings.MovieProSettings.DefaultCredentials;

            var newUser = new IdentityUser()
            {
                Email = credentials.Email,
                UserName = credentials.Email,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(newUser, credentials.Password);

            await _userManager.AddToRoleAsync(newUser, credentials.Role);
        }

        private async Task SeedCollections()
        {
            if (_dbContext.Collection.Any()) return;

            _dbContext.Add(new Collection()
            {
                Name = _appSettings.MovieProSettings.DefaultCollection.Name,
                Description = _appSettings.MovieProSettings.DefaultCollection.Description
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
