using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;
using e_library.Model;

namespace e_library.Model;


public sealed record OrderItemInfo
{
	public BookInfo Book { get; init; }
	
	
  public int Copies { get; init; } = 1;
  
  public OrderItem ToOrderItemEntity(DBContext context)
	{
	  return new OrderItem
	  {
	    Book = context.Books.Include(b => b.Authors)
	                        .Include(b => b.Reviews)
	                        .Include(b => b.Offer)
	                        .First(b => Book.ID == b.ID),
	    Copies = Copies
	  };
	}
	
	public static OrderItemInfo FromOrderItemEntity(OrderItem orderItem)
  {  
    return new OrderItemInfo
    {
	    Book = BookInfo.FromBookEntity(orderItem.Book),
	    Copies = orderItem.Copies
	  };
  }
}

