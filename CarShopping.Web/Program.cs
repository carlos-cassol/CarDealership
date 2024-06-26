using CarShopping.Web.Services;
using CarShopping.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddHttpClient<ICarService, CarService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CarShoppingApi"])
    );

builder.Services.AddHttpClient<ICarDealerService, CarDealerService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CarShoppingApi"])
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
