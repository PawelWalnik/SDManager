using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace PWalnik.SDManager.Interfaces
{
    public interface IAuthenticationService
    {
        Task<SignInResult> PasswordLoginAsync(string email, string password, bool rememberMe, bool lockout);
        Task LoginAsync(string userName, string email);
        Task LogoutAsync();
        Task<bool> IsPasswordCorrect(string password, string userId);
        Task UpdatePassword(string password, string userId);
    }
}
