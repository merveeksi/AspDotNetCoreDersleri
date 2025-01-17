using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options => { options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]); });

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Users/Login"); //Cookie tabanlı kimlik doğrulama

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting(); //yönlendirme (burada sıra önemli)
app.UseAuthentication(); //uygulama beni tanısın
app.UseAuthorization(); //yetkilendirme

SeedData.SeedDatabase(app);

app.MapDefaultControllerRoute(); //controller route

//localhost://posts/react
//localhost://posts/tag/web-programlama

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/Details/{url}",
    defaults: new { controller = "Posts", action = "Details" });

app.MapControllerRoute(
    name: "posts_by_tag",
    pattern: "posts/tag/{tag}",
    defaults: new { controller = "Posts", action = "Index" });

app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new { controller = "Users", action = "Profile" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
