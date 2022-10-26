using e_library.Services;
using e_library.Services.Implementation;
using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;

using e_library;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services
builder.Services.AddScoped<ILibrary, Library>();
builder.Services.AddTransient<ICart, Cart>();
builder.Services.AddTransient<IMailman, Mailman>();
builder.Services.AddSingleton<IOrderNotificationService, OrderNotificationService>();
builder.Services.AddScoped<IStore, Store>();
builder.Services.AddScoped<IAccount, Account>();

// Session
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(10);               
});

// Identity
builder.Services.AddDbContext<DBContext>(options => 
	options.UseSqlite(builder.Configuration["ConnectionStrings:DB"]));
	
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=> {
                //Password options
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                
                // Email options
                options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<DBContext>();

builder.Services.AddAuthorization(options =>
  options.AddPolicy("admin", policy => policy.RequireRole("admin"))
);
// Add Razor Pages
builder.Services.AddRazorPages();

//Add Blazor
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Needed for nginx
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
      ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Blazor
app.MapBlazorHub();
app.MapFallbackToPage("/Admin/_Host");

// Seed Admin User
//await SeedData.AdminAndRoles(app.Services);

app.Run();
