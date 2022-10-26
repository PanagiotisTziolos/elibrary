using System.ComponentModel.DataAnnotations;

namespace e_library.EFC_Database.Entities;

public class Customer
{
  [Key]
  public int ID { get; set; }
  
	public string FirstName { get; set; }

	public string SecondName { get; set; }
	
	public string City { get; set; }

	public string ZipCode { get; set; }

	public string Address { get; set; }
	
	public string EmailAddress { get; set; }
}

