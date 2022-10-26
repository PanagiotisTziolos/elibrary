using Microsoft.EntityFrameworkCore;

using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;
using e_library.Model;

namespace e_library.Model;


public sealed class BookInfo
{
  public int ID { get; init; }
  
	public string Title { get; init; }
	
	public float Price { get; init; }
	
	public string Description { get; init; }
	
    public long Isbn { get; set; }
	
	public string Publisher { get; set; }
	
	public int ReleasedYear { get; set; }
	
	public int AvailableCopies { get; init; }
	
	public ICollection<AuthorInfo> Authors { get; init; }
	
	public ICollection<ReviewInfo> Reviews { get; init; }
	
	public PriceOfferInfo Offer { get; init; }
	
	public Book ToBookEntity(DBContext context)
	{
	  return new Book
    {
      ID = ID,
      Title = Title,
      Price = Price,
      Description = Description,
      Isbn = Isbn,
      Publisher = Publisher,
      ReleasedYear = ReleasedYear,
      Copies = AvailableCopies,
      Authors = Authors.Select(a => a.ToAuthorEntity(context)).ToList(),
      Reviews = Reviews.Select(a => a.ToReviewEntity()).ToList(),
      Offer = Offer.ToPriceOfferEntity()
    };
	}
	
	public static BookInfo FromBookEntity(Book book)
	{
	  return new BookInfo
    {
      ID = book.ID,
      Title = book.Title,
      Price = book.Price,
      Description = book.Description,
      Isbn = book.Isbn,
      Publisher = book.Publisher,
      ReleasedYear = book.ReleasedYear,
      AvailableCopies = book.Copies,
      Authors = book.Authors.Select(a => AuthorInfo.FromAuthorEntity(a)).ToList(),
      Reviews = book.Reviews.Select(r => ReviewInfo.FromReviewEntity(r)).ToList(),
      Offer = PriceOfferInfo.FromPriceOfferEntity(book.Offer)
    };
	}
	
  public bool Exists(DBContext context)
	{
	  foreach (var b in context.Books.Include(b => b.Authors))
	  {
	    if (Equals(b))
	      return true;
	  }
	  
	  return false;
	}
	
	public bool Equals(Book b)
	{
    if (b.Title.Replace(" ", "").ToLower() != Title.Replace(" ", "").ToLower())
      return false;
	  else
      return b.Authors.Where(ba => Authors.FirstOrDefault(a => a.Equals(ba)) is not null)
                      .Count() == Authors.Count();
	}
}

