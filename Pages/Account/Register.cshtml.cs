using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using e_library.Services;
using e_library.Model;

namespace e_library.Pages.Account;

public class RegisterModel: PageModel
{
	public readonly IAccount _account;
	
	public RegisterModel(IAccount account)
	{
    _account = account;
	}
		
	[BindProperty]
	public RegisterInfo Registration { get; set; }
	
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
		  await _account.CreateAsync(Registration);
		  
		  return RedirectToPage("/Index");
		}
		catch (Exception e)
		{
		  ModelState.AddModelError("Register", e.Message);
		  
		  return Page();
		}
	}
}
