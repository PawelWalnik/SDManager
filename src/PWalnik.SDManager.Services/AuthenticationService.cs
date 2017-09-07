using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PWalnik.SDManager.Interfaces;
using PWalnik.SDManager.DataModel.Data;
using PWalnik.SDManager.DataModel.Models;

namespace PWalnik.SDManager.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        
        public AuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            PasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }

        

        public async Task<SignInResult> PasswordLoginAsync(string email, string password, bool rememberMe, bool lockout)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockout);
        }


        public async Task LoginAsync(string userName, string email)
        {
            Random r = new Random();
            var user = new ApplicationUser { UserName = userName, Email = email, SecurityStamp = r.Next().ToString()};
            await _signInManager.SignInAsync(user, isPersistent:false);
            await _context.SaveChangesAsync();
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsPasswordCorrect(string password, string userId)
        {
            if (userId != null)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) !=
                    PasswordVerificationResult.Failed)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task UpdatePassword(string password, string userId)
        {
            if (userId != null)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
                user.PasswordHash = _passwordHasher.HashPassword(user, password);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }        
    }
}
