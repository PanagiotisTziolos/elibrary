using System.ComponentModel.DataAnnotations;

namespace e_library.EFC_Database.Entities;

public class OrderItem
{
  [Key]
	public int ID { get; set; }
	
	public int Copies { get; set; }

  public int BookID { get; set; }
  
	public Book Book { get; set; }
}

