using System.ComponentModel.DataAnnotations;

namespace e_library.EFC_Database.Entities;

public class PriceOffer
{
  [Key]
	public int ID { get; set; }
	
	public float Value { get; set; }
	
	public int BookID { get; set; }
	
	public Book Book { get; set; }
}

