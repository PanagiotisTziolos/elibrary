using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using System.Threading.Tasks;

using e_library.Model;
using e_library.Services;

namespace e_library.Pages;

class BooksModel: PageModel
{
	private readonly ILibrary _library;
	
    private readonly int _pageBooks = 6;
    
    private int _totalBooks;
	
	public BooksModel(ILibrary library)
	{
		_library = library;
	}
	
	public IReadOnlyList<BookInfo> Books { get; set; }
	
	public int TotalPages => (int)Math.Ceiling(1.0 * _totalBooks / _pageBooks);
	
	[BindProperty(SupportsGet = true)]
	public string Title { get; set; }
	
	[BindProperty(SupportsGet = true)]
	public int PageNumber { get; set; } = 0;
		
	[BindProperty]
	public int Id { get; set; }
	
	public async Task<IActionResult> OnGetAsync()
	{
		_totalBooks = await _library.BooksWithTitleCountAsync(Title ?? String.Empty);
	    
	    if (_totalBooks == 0)
		    return RedirectToPage("NotFound");
	    
	    Books = 
	        await _library.BooksWithTitleBatchAsync(Title ?? String.Empty, _pageBooks, PageNumber);

	    return Page();
	}
	
	public async Task<IActionResult> OnPostAsync()
    {
        return RedirectToPage("Books", new { Title = Title, PageNumber = PageNumber });
	}
}
