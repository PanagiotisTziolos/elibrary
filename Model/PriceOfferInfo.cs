using e_library.EFC_Database.Entities;

namespace e_library.Model;


public sealed record PriceOfferInfo
{
	public float Value { get; init; }
	
	public static PriceOfferInfo FromPriceOfferEntity(PriceOffer offer)
	{
	  return new PriceOfferInfo
	  {
	    Value = offer.Value
	  };
	}
	
	public PriceOffer ToPriceOfferEntity()
	{
	  return new PriceOffer { Value = Value };
	}
}

