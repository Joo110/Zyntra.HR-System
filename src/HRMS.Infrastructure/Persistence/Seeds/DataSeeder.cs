using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HRMS.Domain.Constants;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Identity;

namespace HRMS.Infrastructure.Persistence.Seeds;

public static class DataSeeder
{
    public static async Task SeedAsync(
        HrmsDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await SeedRolesAsync(roleManager);

        await SeedSuperAdminUserAsync(userManager);
        await SeedHRManagerUserAsync(userManager);

        await SeedLeaveTypesAsync(context);
        await SeedDepartmentsAsync(context);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[]
        {
            RoleConstants.SuperAdmin,
            RoleConstants.Admin,
            RoleConstants.HRManager,
            RoleConstants.Manager,
            RoleConstants.Employee
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    // =========================
    // Super Admin
    // =========================
    private static async Task SeedSuperAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string email = "superadmin@hrms.com";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = "Super",
                LastName = "Admin",
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(user, "SuperAdmin@12345");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, RoleConstants.SuperAdmin);
            }
        }
    }

    // =========================
    // HR Manager
    // =========================
    private static async Task SeedHRManagerUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string email = "hrmanager@hrms.com";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = "HR",
                LastName = "Manager",
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(user, "HRManager@12345");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, RoleConstants.HRManager);
            }
        }
    }

    // =========================
    // Leave Types
    // =========================
    private static async Task SeedLeaveTypesAsync(HrmsDbContext context)
    {
        if (!await context.LeaveTypes.AnyAsync())
        {
            context.LeaveTypes.AddRange(
                new LeaveType { Code = "AL", Name = "Annual Leave", MaxDaysPerYear = 21, IsPaid = true, CarryForward = true, MaxCarryForwardDays = 5 },
                new LeaveType { Code = "SL", Name = "Sick Leave", MaxDaysPerYear = 14, IsPaid = true },
                new LeaveType { Code = "ML", Name = "Maternity Leave", MaxDaysPerYear = 90, IsPaid = true },
                new LeaveType { Code = "PL", Name = "Paternity Leave", MaxDaysPerYear = 5, IsPaid = true },
                new LeaveType { Code = "UL", Name = "Unpaid Leave", MaxDaysPerYear = 30, IsPaid = false }
            );

            await context.SaveChangesAsync();
        }
    }

    // =========================
    // Departments
    // =========================
    private static async Task SeedDepartmentsAsync(HrmsDbContext context)
    {
        if (!await context.Departments.AnyAsync())
        {
            context.Departments.AddRange(
                new Department { Code = "HR", Name = "Human Resources", Description = "HR Department" },
                new Department { Code = "IT", Name = "Information Technology", Description = "IT Department" },
                new Department { Code = "FIN", Name = "Finance", Description = "Finance Department" },
                new Department { Code = "OPS", Name = "Operations", Description = "Operations Department" }
            );

            await context.SaveChangesAsync();
        }
    }
}