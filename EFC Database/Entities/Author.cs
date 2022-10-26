using System.ComponentModel.DataAnnotations;

namespace e_library.EFC_Database.Entities;


public class Author
{
  [Key]
	public int ID { get; set; }
	
	public string Name { get; set; }
	
	public List<Book> Books { get; set; }
}

