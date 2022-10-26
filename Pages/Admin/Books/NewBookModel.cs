using e_library.Model;

namespace e_library.Pages.Admin.Books;


public class NewBookModel
{
    public int ID { get; set; }
    
    public string Title { get; set; } = String.Empty;
	  
    public float Price { get; set; } = 0.0f;
	  
    public string Description { get; set; } = String.Empty;
	  
    public int AvailableCopies { get; set; } = 1;
	  
    public List<string> Authors { get; init; } = new() { "" };
	  
    public float Offer { get; set; } = 0.0f;
	  
    public BookInfo ToBookInfo()
    {
        return new BookInfo
        {
            ID = ID,
            Title = Title,
            Price = Price,
            Description = Description,
            AvailableCopies = AvailableCopies,
            Authors = Authors.Select(name => new AuthorInfo { Name = name }).ToList(),
            Reviews = new List<ReviewInfo>(),
            Offer = new PriceOfferInfo { Value = Offer }
        };
    }
    
    public static NewBookModel FromBookInfo(BookInfo b)
    {
        return new NewBookModel
        {
            ID = b.ID,
            Title = b.Title,
            Price = b.Price,
            Description = b.Description,
            AvailableCopies = b.AvailableCopies,
            Authors = b.Authors.Select(a => a.Name).ToList(),
            Offer = b.Offer.Value
        };
    }
}
