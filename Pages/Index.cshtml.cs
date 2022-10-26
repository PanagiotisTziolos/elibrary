using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace e_library.Pages;

class IndexModel: PageModel
{
	public IActionResult OnGet()
	{
		return Page();
	}
}
