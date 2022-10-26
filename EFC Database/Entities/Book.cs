using System.ComponentModel.DataAnnotations;

namespace e_library.EFC_Database.Entities;


public class Book
{
  [Key]
	public int ID { get; set; }
	
	public string Title { get; set; }
	
	public float Price { get; set; }
	
	public string Description { get; set; }
	
	public long Isbn { get; set; }
	
	public string Publisher { get; set; }
	
	public int ReleasedYear { get; set; }
	
	public int Copies { get; set; }
	
	public ICollection<Author> Authors { get; set; }
	
	public ICollection<Review> Reviews { get; set; }
	
	public PriceOffer Offer { get; set; }
}

