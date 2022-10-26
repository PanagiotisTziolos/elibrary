using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using e_library.Model;
using e_library.EFC_Database.Entities;

namespace e_library.EFC_Database.Context;

public class DBContext : IdentityDbContext<ApplicationUser>
{
  public DbSet<Book> Books { get; set; }
  public DbSet<Author> Authors { get; set; }
  public DbSet<Review> Reviews { get; set; }
  public DbSet<PriceOffer> PriceOffers { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<Customer> Customers { get; set; }
  
	public DBContext(DbContextOptions<DBContext> options): base(options) {}
  
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// add books
		InitialDataList list = new();
		
		  modelBuilder.Entity<PriceOffer>().HasData(
		    new PriceOffer { ID = 1, Value = 0, BookID = 1 },
		    new PriceOffer { ID = 2, Value = 0, BookID = 2 },
		    new PriceOffer { ID = 3, Value = 0, BookID = 3 }
		  );
    	
    	modelBuilder.Entity<Book>()
				.HasData(list.Books);
    	
    	modelBuilder.Entity<Author>()
    		.HasData(list.Authors);
    			
    	modelBuilder.Entity<Book>()
    		.HasMany(b => b.Authors)
    		.WithMany(a => a.Books)
    		.UsingEntity(j => j.ToTable("BookAuthor")
    			.HasData( 
    				new { BooksID = list.Books[0].ID, AuthorsID = list.Authors[0].ID },
    				new { BooksID = list.Books[1].ID, AuthorsID = list.Authors[1].ID },
    				new { BooksID = list.Books[2].ID, AuthorsID = list.Authors[2].ID }
    			)
    		);
	}
  
  // Class that contains initial Data
		private class InitialDataList
		{
			public IReadOnlyList<Book> Books { get; }
			public IReadOnlyList<Author> Authors { get; }
			
			public InitialDataList()
			{
				Books = new List<Book>
				{
					new Book
					{
						ID = 1,
						Title = "ASP.NET Core in Action 2",
						Price = 43.99f,
						Description = @"ASP.NET Core in Action, Second Edition is a comprehensive guide to creating web applications with ASP.NET Core 5.0. Go from basic HTTP concepts to advanced framework customization. Illustrations and annotated code make learning visual and easy. Master logins, dependency injection, security, and more. This updated edition covers the latest features, including Razor Pages and the new hosting paradigm.",
					  Copies = 10,
					  Isbn = 9781617298301,
					  Publisher = "Manning",
					  ReleasedYear = 2021
					},
					new Book
					{
						ID = 2,
						Title = "Effective Java",
						Price = 37.45f,
						Description = @"Java has changed dramatically since the previous edition of Effective Java was published shortly after the release of Java 6. This Jolt award-winning classic has now been thoroughly updated to take full advantage of the latest language and library features. The support in modern Java for multiple paradigms increases the need for specific best-practices advice, and this book delivers.",
					  Copies = 10,
					  Isbn = 9780134685991,
					  Publisher = "Addison-Wesley",
					  ReleasedYear = 2018
					},
					new Book
					{
						ID = 3,
						Title = " JavaScript: The Definitive Guide",
						Price = 70.0f,
						Description = @"JavaScript is the programming language of the web and is used by more software developers today than any other programming language. For nearly 25 years this best seller has been the go-to guide for JavaScript programmers. The seventh edition is fully updated to cover the 2020 version of JavaScript, and new chapters cover classes, modules, iterators, generators, Promises, async/await, and metaprogramming. You’ll find illuminating and engaging example code throughout. This book is for programmers who want to learn JavaScript and for web developers who want to take their understanding and mastery to the next level. It begins by explaining the JavaScript language itself, in detail, from the bottom up. It then builds on that foundation to cover the web platform and Node.js.",
					  Copies = 10,
					  Isbn = 9781491952023,
					  Publisher = "OReilly",
					  ReleasedYear = 2020
					}
				}.AsReadOnly();
				
				Authors = new List<Author>
				{
					new Author { ID = 1, Name = "Andrew Lock" },
					new Author { ID = 2, Name = "Joshua Bloch" },
					new Author { ID = 3, Name = "David Flanagan" },
				}.AsReadOnly();
			}
		}
}

