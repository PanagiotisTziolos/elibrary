using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace e_library.Pages;

class OrderCompletedModel: PageModel
{	
	public OrderCompletedModel()
	{
		ThankingMessage = @"Thank you for your order";
		InfoMessage = @"You will receive an email with information regarding your order";
	}
	
	public string ThankingMessage { get; }
	public string InfoMessage { get; }
	
	public IActionResult OnGet()
	{		
		return Page();
	}
}
