global using BookStoreWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookstoreContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));
//cau hinh cookie
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
//cau hinh xac thuc nguoi dung bang cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "bookstore.cookie",
        Path = "/"
    };
    options.LoginPath = new PathString("/LoginReg/Index");
    options.ReturnUrlParameter = "urlRedirect";
    options.SlidingExpiration = true;
});
//builder.Services.AddDbContext<BookstoreContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//su dung policy cookie
app.UseCookiePolicy();
//su dung authentication trong asp.net core
app.UseAuthentication();
//
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
