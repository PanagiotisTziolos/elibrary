using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace e_library.Pages;

class NotFoundModel: PageModel
{
	public IActionResult OnGet()
	{
		return Page();
	}
}
