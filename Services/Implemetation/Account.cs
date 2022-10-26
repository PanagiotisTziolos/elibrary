using Microsoft.AspNetCore.Identity;

using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;
using e_library.Services;
using e_library.Model;

namespace e_library.Services.Implementation;


public sealed class Account: IAccount
{
  private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly DBContext _context;
	
	public Account(DBContext context,
	  UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
	{
	  _context = context;
    _userManager = userManager;
    _signInManager = signInManager;
  }
  
  public async Task CreateAsync(RegisterInfo Registration)
  {
    var existingUser = await _userManager.FindByEmailAsync(Registration.Customer.EmailAddress);
    
    if (existingUser is not null)
      throw new Exception("A user with that email already exists");
    
    var creation = await _userManager.CreateAsync(
			new ApplicationUser
			{
				UserName = Registration.Customer.EmailAddress,
				Email = Registration.Customer.EmailAddress,
				// Customer Info
				Info = Registration.Customer.ToCustomerEntity(_context)
			},
			Registration.Password);
		
	  if (!creation.Succeeded)
		  throw new Exception("There was an error creating your account");
  }

  public async Task LogInAsync(LoginInfo Login)
  {
    var signIn = await _signInManager.PasswordSignInAsync(
			  Login.EmailAddress, Login.Password, Login.Remember, false);
	  
    if (signIn.IsNotAllowed)
      throw new Exception("The account is not verified");
    
    if (!signIn.Succeeded)
      throw new Exception("Email - Password combination is wrong");
  }
        
  public async Task LogOutAsync()
  {
    await _signInManager.SignOutAsync();
  }
}

