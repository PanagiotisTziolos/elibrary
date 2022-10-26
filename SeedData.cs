using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using e_library.EFC_Database.Entities;
using e_library.EFC_Database.Context;

namespace e_library;

public sealed class SeedData
{
  public static async Task AdminAndRoles(IServiceProvider serviceProvider)
  {
    using(var scope = serviceProvider.CreateScope())
    {
      var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
      var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
      var dbContext = scope.ServiceProvider.GetService<DBContext>();

      // Create the Roles
      var result = await roleManager.CreateAsync(new IdentityRole { Name = "admin" });
      var result2 = await roleManager.CreateAsync(new IdentityRole { Name = "customer" });
      
      // Create an admin user
      var admin = new ApplicationUser
      { 
          Email = "admin@elibrary.com",
          EmailConfirmed = true,
          UserName = "admin@elibrary.com",
          Info = new Customer()
          {
            FirstName = "ADMIN",
            SecondName = String.Empty,
            City = String.Empty,
            ZipCode = String.Empty,
            Address = String.Empty,
            EmailAddress = "admin@elibrary.com"
          }
      };
        
      string password = "ADMIN";
        
      string hashedPassword = new PasswordHasher<ApplicationUser>().HashPassword(admin, password);
        
      admin.PasswordHash = hashedPassword;
        
      var result3 = await userManager.CreateAsync(admin);

      // Add admin to ADMIN Role
      await userManager.AddToRoleAsync(admin, "admin");
        
      // Save changes to database
      await dbContext.SaveChangesAsync();
    }
  }
}
