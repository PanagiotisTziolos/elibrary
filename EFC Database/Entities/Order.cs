using System.ComponentModel.DataAnnotations;

namespace e_library.EFC_Database.Entities;

//public enum OrderState { READY, CANCELLED, DELIVERED, PENDING }


public class Order
{
  [Key]
	public int ID { get; set; }
	
  public string State { get; set; }
  
  public Customer Owner { get; set; }
	
	public ICollection<OrderItem> Items { get; set; }
}

