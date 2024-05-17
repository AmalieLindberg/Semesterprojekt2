using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.EFDbContext;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService;
using Semesterprojekt2.Service.UserService.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<ProductOrderService, ProductOrderService>();
builder.Services.AddSingleton<IBookATimeService, BookATimeService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddTransient<JsonFileProductService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<YdelseService, YdelseService>();
builder.Services.AddSingleton<UserService, UserService>();
builder.Services.AddSingleton<DogService, DogService>();
builder.Services.AddSingleton<BookedDaysService, BookedDaysService>();
builder.Services.AddTransient<JsonFileUserService>();
builder.Services.AddDbContext<SemsterprojektDbContext>();

builder.Services.AddSingleton<ShoppingCartService, ShoppingCartService>();

builder.Services.AddTransient<UserDbService, UserDbService>();
builder.Services.AddTransient<DbGenericService<Product>, DbGenericService<Product>>();
builder.Services.AddTransient<DbGenericService<BookedDays>, DbGenericService<BookedDays>>();
builder.Services.AddTransient<BookATimeDbService, BookATimeDbService>();
builder.Services.AddTransient<DbGenericService<Dog>, DbGenericService<Dog>>();
builder.Services.AddTransient<DbGenericService<Ydelse>, DbGenericService<Ydelse>>();
builder.Services.AddTransient<DbGenericService<ProductOrder>, DbGenericService<ProductOrder>>();
//builder.Services.AddTransient<DbGenericService<CartItem>, DbGenericService<CartItem>>();

builder.Services.Configure<CookiePolicyOptions>(options => {
    // This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.LoginPath = "/Login/Login";

});
//builder.Services.AddMvc().AddRazorPagesOptions(options => {
    //options.Conventions.AuthorizeFolder("/Item");

//}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

var app = builder.Build();

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
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
