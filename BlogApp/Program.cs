using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration; 
    var connectionString = config.GetConnectionString("sql_connection"); 
    options.UseSqlite(); 
});

app.MapGet("/", () => "Hello World!");

app.Run();
