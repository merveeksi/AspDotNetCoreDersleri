using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore;

public static class SeedData
{
    public static void SeedDatabase(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
        if (context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            if (!context.Tags.Any())
                context.Tags.AddRange(
                    new Tag { Text = "Web Programlama" },
                    new Tag { Text = "Backend" },
                    new Tag { Text = "Frontend" },
                    new Tag { Text = "Fullstack" },
                    new Tag { Text = "PHP" });
            context.SaveChanges();
        }

        if (!context.Users.Any())
            context.Users.AddRange(
                new User { UserName = "Merve Ekşi" },
                new User { UserName = "Alp Üstün" },
                new User { UserName = "Eylül Kırmmızıyüz" });
        context.SaveChanges();

        if (!context.Posts.Any())
            context.Posts.AddRange(
                new Post
                {
                    Title = "ASP.NET Core 7.0",
                    Content = "ASP.NET Core 7.0 hakkında bilgiler",
                    IsActive = true,
                    PublisedOn = DateTime.Now.AddDays(-10),
                    Tags = context.Tags.Take(3).ToList(),
                    Image = "1.jpg",
                    UserId =1
                },
                new Post
                {
                    Title = "Entity Framework Core 7.0",
                    Content = "Entity Framework Core 7.0 hakkında bilgiler",
                    IsActive = true,
                    PublisedOn = DateTime.Now.AddDays(-20),
                    Tags = context.Tags.Take(2).ToList(),
                    Image = "2.jpg",
                    UserId = 1
                    //User = context.Users.FirstOrDefault()   //ikinci kullanma şekli
                    
                },
                new Post
                {
                    Title = "Blazor",
                    Content = "Blazor hakkında bilgiler",
                    IsActive = true,
                    PublisedOn = DateTime.Now.AddDays(-5),
                    Tags = context.Tags.Take(4).ToList(),
                    Image = "3.jpg",
                    UserId = 1
                });
        context.SaveChanges();
    }
}