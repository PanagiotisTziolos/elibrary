using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using e_library.Services;
using e_library.Model;

namespace e_library.Pages.Account;

public class LoginModel: PageModel
{
	private readonly IAccount _account;
	
	public LoginModel(IAccount account)
	{
    _account = account;
	}
	
	[BindProperty]
	public LoginInfo Login { get; set; }
	
	public IActionResult OnGet()
	{
		return Page();
	}
	
	public async Task<IActionResult> OnPost()
	{
		if (!ModelState.IsValid)
			return Page();
		
		try
		{
            await _account.LogInAsync(Login);

            return RedirectToPage("/Index");
        }
        catch (Exception e)
        {
            ModelState.AddModelError("LogIn", e.Message);
            
            return Page();
        }
	}
}
