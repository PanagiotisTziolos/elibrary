using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;
using e_library.Model;

namespace e_library.Model;


public sealed class CustomerInfo
{
  [Display(Name="First Name")]
  [StringLength(30)]
	public string FirstName { get; init; }
	
	[Display(Name="Second Name")]
	[StringLength(30)]
	public string SecondName { get; init; }
	
	[StringLength(100)]
	public string City { get; init; }
	
	[Display(Name="Zip Code")]
	[RegularExpression(
	  @"\d{5}",
	  ErrorMessage = "The zip code must be a 5 digit number")]
	public string ZipCode { get; init; }
	
	[StringLength(100)]
	public string Address { get; init; }
	
	[Display(Name="Email Address")]
	[EmailAddress(ErrorMessage = "Not a valid email address")]
	public string EmailAddress { get; init; }
	
	public Customer ToCustomerEntity(DBContext context)
	{
	  var customer = context.Customers.FirstOrDefault(c =>
                                      c.FirstName == FirstName &&
                                      c.SecondName == SecondName &&
                                      c.City == City &&
                                      c.ZipCode == ZipCode &&
                                      c.Address == Address &&
                                      c.EmailAddress == EmailAddress);
	  return customer ?? new Customer
	                      {
	                        FirstName = FirstName,
	                        SecondName = SecondName,
	                        City = City,
	                        ZipCode = ZipCode,
	                        Address = Address,
	                        EmailAddress = EmailAddress
	                      };
	}
	
	public static CustomerInfo FromCustomerEntity(Customer info)
  {
    return new CustomerInfo
    {
      FirstName = info.FirstName,
      SecondName = info.SecondName,
      City = info.City,
      ZipCode = info.ZipCode,
      Address = info.Address,
      EmailAddress = info.EmailAddress
    };
  }
}

