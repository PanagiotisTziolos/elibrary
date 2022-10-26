using e_library.Model;

namespace e_library.Services;

public interface ILibrary
{	
	public Task<IReadOnlyList<BookInfo>> BooksWithTitleAsync(string title);
	
	public Task<IReadOnlyList<BookInfo>> BooksWithTitleBatchAsync(string title, int pageSize, int pageNumber);
	
	public Task<BookInfo> BookAsync(int id);
	
	public Task<int> LastBookIDAsync();
	
	public void AddReview(int bookID, ReviewInfo reviewInfo);
	
	public void AddBook(BookInfo book);
	
	public void UpdateBook(BookInfo bookInfo);
	
	public void RestoreOrderItems(ICollection<OrderItemInfo> items);
	
	public Task<int> BooksWithTitleCountAsync(string title);
}
