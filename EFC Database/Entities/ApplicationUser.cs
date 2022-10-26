using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace e_library.EFC_Database.Entities;


public class ApplicationUser: IdentityUser
{
	public int CustomerID { get; set; }
	
	public Customer Info { get; set; }
}

