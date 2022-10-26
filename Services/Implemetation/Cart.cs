using Microsoft.AspNetCore.Http;

using System.Collections.Generic;

using e_library.Model;
using e_library.Services;
using e_library.Extentions;

namespace e_library.Services.Implementation;


public sealed class Cart: ICart
{
	private readonly ILibrary _library;
	private readonly HttpContext? _httpContext;
	private readonly List<OrderItemInfo> _items;
	
	public Cart(ILibrary library, IHttpContextAccessor httpContextAccessor)
	{
        _library = library;
		_httpContext = httpContextAccessor.HttpContext;
		_items = httpContextAccessor.HttpContext.Session.Get<List<OrderItemInfo>>("shoplist") ?? 
            new List<OrderItemInfo>();
	}

	public IReadOnlyList<OrderItemInfo> Items
        => _items.AsReadOnly();
	
	public async Task Add(int id)
	{
        var existingBook = _items.FirstOrDefault(oi => oi.Book.ID == id);
	  
        if (existingBook is null)
            _items.Add(
                new OrderItemInfo
                {
                  Book = await _library.BookAsync(id),
                  Copies = 1
                }
            );
    
        UpdateSession();
	}
	
	public void Remove(int id)
	{
	    Console.WriteLine(id);
        _items.Remove(_items.First(i => i.Book.ID == id));
        
		UpdateSession();
	}
	
	public void Update(IReadOnlyList<OrderItemInfo> orderItems)
	{	  
        _items.Clear();
	  
        _items.AddRange(orderItems);
        
		UpdateSession();
	}
	
	public void Clear()
	{
		_items.Clear();
		UpdateSession();
	}
	
	private void UpdateSession()
		=> _httpContext.Session?.Set<List<OrderItemInfo>>("shoplist", _items);
}

