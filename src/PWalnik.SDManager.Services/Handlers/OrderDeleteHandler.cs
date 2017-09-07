using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.Services.Requirements;

namespace PWalnik.SDManager.Services.Handlers
{
    public class OrderDeleteHandler : AuthorizationHandler<DeleteOrderRequirement, Order>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteOrderRequirement requirement, Order resource)
        {
            if (resource.EmployeeId == context.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
