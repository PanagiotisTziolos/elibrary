using System.ComponentModel.DataAnnotations;

namespace e_library.Model;


public sealed record LoginInfo
{
	[Display(Name="Email Address")]
	[EmailAddress(ErrorMessage = "Not a valid email address")]
	public string EmailAddress { get; init; }
	
	[Display(Name="Password")]
	public string Password { get; init; }
	
	[Display(Name="Remember me")]
	public bool Remember { get; init; }
}

