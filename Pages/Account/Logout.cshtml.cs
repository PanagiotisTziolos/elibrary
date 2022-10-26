using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using e_library.Services;

namespace e_library.Pages.Account;

public class LogoutModel: PageModel
{
	public readonly IAccount _account;
	
	public LogoutModel(IAccount account)
	{
		_account = account;
	}
	
	public async Task<IActionResult> OnGet()
	{
		await _account.LogOutAsync();
		
		return RedirectToPage("/Index");
	}
}
