using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using e_library.EFC_Database.Entities;
using e_library.Services;
using e_library.Model;

namespace e_library.Pages;

public sealed class CompleteOrder: PageModel
{
	private readonly ICart _cart;
	private readonly IMailman _mailman;
	private readonly IOrderNotificationService _notificationService;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IStore _store;
	
	public CompleteOrder(ICart cart, IMailman mailman, IStore store,
	  IOrderNotificationService notificationService, UserManager<ApplicationUser> userManager) 
	{
		_cart = cart;
		_mailman = mailman;
		_notificationService = notificationService;
		_userManager = userManager;
		_store = store;
	}
	  
	public ApplicationUser LoggedInUser { get; set; }
	
	public IReadOnlyList<OrderItemInfo> Items
	    => _cart.Items;
	
	[BindProperty]
	public CustomerInfo Customer { get; set; }
	
	[BindProperty]
	public string DeliveryOption { get; set; }
	
	public async Task<IActionResult> OnGetAsync()
	{	  
	    LoggedInUser = await LoggedUser();
    
		return Page();
	}
	
	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
		    LoggedInUser = await LoggedUser();
			return Page();
		}
		
		try
		{
            _mailman.Send(
                new EmailInfo
                {
                  OrderItems = _cart.Items,
                  Address = Customer.EmailAddress
                }
            );
		}
		catch (Exception e)
		{
		    LoggedInUser = await LoggedUser();

			ModelState.AddModelError("Email", 
			  "There was an error sending the email. Please your internet connection or try again later.");
			return Page();
		}
		
        _store.AddOrder(
            new OrderInfo
            {
                Customer = Customer,
                Items = _cart.Items.Select(i =>
                    new OrderItemInfo { Book = i.Book, Copies = i.Copies }
                ).ToList()
            }
        );
    
        _notificationService.Notify();
			
        _cart.Clear();

        return RedirectToPage("OrderCompleted");
	}
	
	private async Task<ApplicationUser?> LoggedUser()
	{
        if (User.Identity.IsAuthenticated)
            return await _userManager.Users.Include(appUser => appUser.Info)
                        .SingleAsync(appUser => appUser.Email == User.Identity.Name);
                        
        return null;
    }
}
