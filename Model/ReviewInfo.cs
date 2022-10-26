using e_library.EFC_Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace e_library.Model;


public sealed class ReviewInfo
{
    [Required]
	public int Rating { get; init; }
	
	[Required(ErrorMessage = "A review can't contain only rating")]
	public string Content { get; init; }
	
	public static ReviewInfo FromReviewEntity(Review review)
	{
	  return new ReviewInfo
	  {
	    Content = review.Content,
	    Rating = review.Rating
	  };
	}
	
	public Review ToReviewEntity()
	{
	  return new Review { Content = Content, Rating = Rating };
	}
}

