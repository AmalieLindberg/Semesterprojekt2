using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IBookATimeService, BookATimeService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddTransient<JsonFileProductService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<YdelseService, YdelseService>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
