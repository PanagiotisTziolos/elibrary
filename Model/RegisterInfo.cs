using System.ComponentModel.DataAnnotations;

using e_library.Model;

namespace e_library.Model;


public sealed record RegisterInfo
{	
	public CustomerInfo Customer { get; init; }
	
	[Display(Name="Password")]
	[RegularExpression(
		@"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+.?]).*$", 
		ErrorMessage = "Password must contain 8 to 20 characters and at least:\n one lower case character\n one upper case character\n one digit\n one of the special characters [!@#$%^&*()_+.?]")]
	public string Password { get; init; }

	[Display(Name="Confirm Password")]
	[Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
	public string ConfirmPassword { get; init; }
}

