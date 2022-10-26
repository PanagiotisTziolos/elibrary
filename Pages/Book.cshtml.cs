using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

using e_library.Model;
using e_library.Services;

namespace e_library.Pages;


public sealed class BookModel: PageModel
{
	private readonly ILibrary _library;
	private readonly ICart _cart;
	
	public BookModel(ILibrary library, ICart cart)
	{
		_library = library;
		_cart = cart;
	}
	
	[BindProperty(SupportsGet = true)]
	public int Id { get; set; }
	
	[BindProperty]
	public ReviewInfo Review { get; set; }
	
	public BookInfo RequestedBook { get; set; }
	
	public async Task<IActionResult> OnGetAsync()
	{
		try
		{
			RequestedBook = await _library.BookAsync(Id);
			
			return Page();
		}
		catch
		{
			return RedirectToPage("NotFound");
		}
	}
	
	public IActionResult OnPostToCart()
	{
        _cart.Add(Id);
    
	    return RedirectToPage("Book", new { Id = Id });
	}
		
	public async Task<IActionResult> OnPostReview()
	{
		if(!ModelState.IsValid)
		{
		    RequestedBook = await _library.BookAsync(Id);
      
			return Page();
		}

		//Add review to the db
		_library.AddReview(Id, Review);
		
		return RedirectToPage("Book", new { Id = Id });
	}
}


