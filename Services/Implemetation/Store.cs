using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using e_library.Model;
using e_library.Services;
using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;

namespace e_library.Services.Implementation;


public sealed class Store: IStore
{
	private DBContext _context;
	
	public Store(DBContext context)
	{
		_context = context;
	}
	
	private async Task<List<OrderInfo>> OrderThatAre(OrderState state)
	{
	  return await _context.Orders.Where(o => o.State == state.ToString())
	                                    .Include(o => o.Owner)
	                                    .Include(o => o.Items)
	                                      .ThenInclude(i => i.Book)
	                                        .ThenInclude(b => b.Authors)
	                                    .Include(o => o.Items)
	                                      .ThenInclude(i => i.Book)
	                                        .ThenInclude(b => b.Reviews)
	                                    .Include(o => o.Items)
	                                      .ThenInclude(i => i.Book)
	                                        .ThenInclude(b => b.Offer)
                                      .AsNoTracking()
	                                    .Select(o => OrderInfo.FromOrderEntity(o))
	                                    .ToListAsync();
	}
	
	public async Task<IReadOnlyList<OrderInfo>> AllOrdersAsync()
	{
	  var orders = await _context.Orders.Include(o => o.Owner)
	                                    .Include(o => o.Items)
	                                      .ThenInclude(i => i.Book)
	                                        .ThenInclude(b => b.Authors)
	                                    .Include(o => o.Items)
	                                      .ThenInclude(i => i.Book)
	                                        .ThenInclude(b => b.Reviews)
	                                    .Include(o => o.Items)
	                                      .ThenInclude(i => i.Book)
	                                        .ThenInclude(b => b.Offer)
                                      .AsNoTracking()
	                                    .Select(o => OrderInfo.FromOrderEntity(o))
	                                    .ToListAsync();
    
    return orders.AsReadOnly();
  }
  
  public async Task<IReadOnlyList<OrderInfo>> PendingOrdersAsync()
	{
    return (await OrderThatAre(OrderState.PENDING)).AsReadOnly();
  }
  
  public async Task<IReadOnlyList<OrderInfo>> ReadyOrdersAsync()
	{
    return (await OrderThatAre(OrderState.READY)).AsReadOnly();
  }
  
  public async Task<IReadOnlyList<OrderInfo>> DeliveredOrdersAsync()
	{
    return (await OrderThatAre(OrderState.DELIVERED)).AsReadOnly();
  }
  
  public async Task<IReadOnlyList<OrderInfo>> CancelledOrdersAsync()
	{
	  return (await OrderThatAre(OrderState.CANCELLED)).AsReadOnly();
  }
	
	public void AddOrder(OrderInfo orderInfo)
	{
	  Order order = orderInfo.ToOrderEntity(_context);
	  
	  foreach (var item in order.Items)
	    item.Book.Copies -= item.Copies;
	  
	  _context.Orders.Add(order);
    
	  _context.SaveChanges();
	}

	public void ChangeTo(int id, OrderState state)
	{
	  var order = _context.Orders.Where(o => o.ID == id).First();
		order.State = state.ToString();
		
		_context.SaveChanges();
	}
}

