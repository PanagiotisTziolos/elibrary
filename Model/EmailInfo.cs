using System.Text;

using e_library.Model;

namespace e_library.Model;


public sealed class EmailInfo
{
  public IReadOnlyList<OrderItemInfo> OrderItems { get; init; }
  
  public string Subject { get; init; } = "E-Library Order";
  
  public string Address { get; init; }
  
  public string Message { get; init; }
  
  public string Body
  {
    get
    {
      StringBuilder builder = new StringBuilder();
      
      builder.AppendLine(Message);
			
			if (OrderItems is not null)
			{
        foreach (var item in OrderItems)
          builder.Append($@"Book: {item.Book.Title}, Copies: {item.Copies}");
      }
      
      return builder.ToString();
    }
  }
}
