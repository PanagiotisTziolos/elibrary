using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

using e_library.Model;
using e_library.Services;
using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;

namespace e_library.Services.Implementation;


public sealed class Library: ILibrary
{
	private readonly DBContext _context;
	
	public Library(DBContext context)
	{
		_context = context;
	}
	
	public async Task<IReadOnlyList<BookInfo>> BooksWithTitleAsync(string title)
	{
		var b = await _context.Books.Where(book => book.Title.ToLower().Contains(title.ToLower()))
                                    .Include(book => book.Authors)
									.Include(book => book.Reviews)
									.Include(book => book.Offer).AsNoTracking()
									.Select(b => BookInfo.FromBookEntity(b))
									.ToListAsync();
													
		return b.AsReadOnly();
	}
	
	public async Task<IReadOnlyList<BookInfo>> BooksWithTitleBatchAsync(string title, int pageSize, int pageNumber)
	{
		var b = await _context.Books.Where(book => book.Title.ToLower().Contains(title.ToLower()))
                                    .Include(book => book.Authors)
									.Include(book => book.Reviews)
									.Include(book => book.Offer)
									.AsNoTracking()
									.OrderBy(book => book.ID)
									.Skip(pageNumber * pageSize)
									.Take(pageSize)
									.Select(b => BookInfo.FromBookEntity(b))
									.ToListAsync();
													
		return b.AsReadOnly();
	}
	
	public async Task<BookInfo> BookAsync(int id)
	{
		return await _context.Books.Where(book => book.ID == id)
									.Include(book => book.Authors)
									.Include(book => book.Reviews)
									.Include(book => book.Offer).AsNoTracking()
									.Select(b => BookInfo.FromBookEntity(b))
									.FirstAsync<BookInfo>();
	}
	
	public void AddReview(int bookID, ReviewInfo reviewInfo)
	{
        _context.Reviews.Add(
            new Review
	        {
	          Rating = reviewInfo.Rating,
	          Content = reviewInfo.Content,
	          BookID = bookID
	        }
        );
    
        _context.SaveChanges();
	}
	
	public void AddBook(BookInfo bookInfo)
	{
        if (!bookInfo.Exists(_context))
            _context.Books.Add(bookInfo.ToBookEntity(_context));
	  
        _context.SaveChanges();
	}
	
	public void UpdateBook(BookInfo bookInfo)
	{
        var book = _context.Books.Include(b => b.Authors).Include(b => b.Offer)
                                .FirstOrDefault(b => b.ID == bookInfo.ID);
	  
        book.Title = bookInfo.Title;
        book.Price = bookInfo.Price;
        book.Description = bookInfo.Description;
        book.Copies = bookInfo.AvailableCopies;
        book.Authors = bookInfo.Authors.Select(a => a.ToAuthorEntity(_context)).ToList();
        book.Offer = bookInfo.Offer.ToPriceOfferEntity();
	  
        _context.SaveChanges();
	}
	
	public void RestoreOrderItems(ICollection<OrderItemInfo> items)
	{
        foreach (var item in items)
        {
            var book = _context.Books.First(b => b.ID == item.Book.ID);
	    
            book.Copies += item.Copies;
        }
    
        _context.SaveChanges();
	}
	
    public async Task<int> LastBookIDAsync()
    {
		return await _context.Books.MaxAsync(book => book.ID);
	}
	
	public async Task<int> BooksWithTitleCountAsync(string title)
    {
        return await _context.Books.CountAsync(book =>
             book.Title.ToLower().Contains(title.ToLower()));
	}
}



