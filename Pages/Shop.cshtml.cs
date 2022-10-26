using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;

using e_library.Services;
using e_library.Model;

namespace e_library.Pages;

public sealed class ShopModel: PageModel
{
  private readonly List<OrderItemInfo> _items;
	private readonly ICart _cart;
	
	public ShopModel(ICart cart)
	{
        _items = new List<OrderItemInfo>();
		_cart = cart;
	}
	
	public IReadOnlyList<OrderItemInfo> Items 
        => _items.AsReadOnly();
    
    [BindProperty]
    public List<int> Copies { get; set; }

	public IActionResult OnGet()
	{
        _items.AddRange(_cart.Items);
    
		return Page();
	}

	public IActionResult OnPostBuy()
	{
        var items = _cart.Items.Select((i, index) => 
            new OrderItemInfo { Book = i.Book, Copies = Copies[index] }).ToList();
		
		_cart.Update(items);
		
		return RedirectToPage("CompleteOrder");
	}
	
	public IActionResult OnPostRemove(int id)
	{
		_cart.Remove(id);
		
		return RedirectToPage("Shop");
	}
}

