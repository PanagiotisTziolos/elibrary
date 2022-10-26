using System.ComponentModel.DataAnnotations;

namespace e_library.EFC_Database.Entities;

public class Review
{
  [Key]
	public int ID { get; set; }
	
	public int Rating { get; set; }
	
	public string Content { get; set; }
	
	public int BookID { get; set; }
	
	public Book Book { get; set; }
}

