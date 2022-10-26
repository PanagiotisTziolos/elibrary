using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;
using e_library.Model;

namespace e_library.Model;


public sealed class OrderInfo
{
	public int ID { get; init; }
	
  public string State { get; init; } = OrderState.PENDING.ToString();
  
  public CustomerInfo Customer { get; init; }
	
	public ICollection<OrderItemInfo> Items { get; init; }
	
	public Order ToOrderEntity(DBContext context)
	{
	  return new Order
	  {
	    State = State,
	    Owner = Customer.ToCustomerEntity(context),
	    Items = Items.Select(i => i.ToOrderItemEntity(context)).ToList()
	  };
	}
	
	public static OrderInfo FromOrderEntity(Order order)
  {
    return new OrderInfo
    {
      ID = order.ID,
      State = order.State,
      Customer = CustomerInfo.FromCustomerEntity(order.Owner),
      Items = order.Items.Select(i => OrderItemInfo.FromOrderItemEntity(i)).ToList()
    };
  }
}

