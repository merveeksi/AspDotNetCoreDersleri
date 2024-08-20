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
                    new Tag { Text = "Web Programlama", Url = "web-programlama", Color= TagColors.warning},
                    new Tag { Text = "Backend", Url = "backend", Color= TagColors.primary},
                    new Tag { Text = "Frontend", Url = "frontend", Color= TagColors.success},
                    new Tag { Text = "Fullstack", Url = "fullstack", Color= TagColors.danger},
                    new Tag { Text = "PHP", Url = "php", Color= TagColors.secondary});
            context.SaveChanges();
        }

        if (!context.Users.Any())
            context.Users.AddRange(
                new User { UserName = "Merve Ekşi", Image= "p1.jpg"},
                new User { UserName = "Alp Giray Üstün", Image = "p2.jpg"},
                new User { UserName = "Eylül Kırmmızıyüz", Image= "p1.jpg" });
        context.SaveChanges();

        if (!context.Posts.Any())
            context.Posts.AddRange(
                new Post
                {
                    Title = "ASP.NET Core 7.0",
                    Content = "ASP.NET Core 7.0 hakkında bilgiler",
                    Url ="asp-net-core",
                    IsActive = true,
                    PublishedOn = DateTime.Now.AddDays(-10),
                    Tags = context.Tags.Take(3).ToList(),
                    Image = "1.jpg",
                    UserId =1,
                    Comments = new List<Comment>{
                        new Comment {Text = "Çok güzel bir kurs", 
                            PublishedOn = DateTime.Now.AddDays(-10),
                            UserId = 1},
                            new Comment {Text = "Temelimi iyi atabildiğim bir kurs", 
                            PublishedOn = DateTime.Now.AddDays(-20),
                            UserId = 2},
                            new Comment {Text = "Herkese tavsiye ederim", 
                            PublishedOn = DateTime.Now.AddDays(-30),
                            UserId = 3 }
                    } //yorum eklemek için
                },
                new Post
                {
                    Title = "Entity Framework Core 7.0",
                    Content = "Entity Framework Core 7.0 hakkında bilgiler",
                    Url = "entity-framework-core",
                    IsActive = true,
                    PublishedOn = DateTime.Now.AddDays(-20),
                    Tags = context.Tags.Take(2).ToList(),
                    Image = "2.jpg",
                    UserId = 1
                    //User = context.Users.FirstOrDefault()   //ikinci kullanma şekli
                    
                },
                new Post
                {
                    Title = "Blazor",
                    Content = "Blazor hakkında bilgiler",
                    Url = "blazor",
                    IsActive = true,
                    PublishedOn = DateTime.Now.AddDays(-5),
                    Tags = context.Tags.Take(4).ToList(),
                    Image = "3.jpg",
                    UserId = 1
                }, 
                new Post
                {
                    Title = "React Dersleri",
                    Content = "React hakkında bilgiler",
                    Url = "react",
                    IsActive = true,
                    PublishedOn = DateTime.Now.AddDays(-30),
                    Tags = context.Tags.Take(4).ToList(),
                    Image = "3.jpg",
                    UserId = 1
                },
                new Post
                {
                    Title = "Angular",
                    Content = "Angular hakkında bilgiler",
                    Url = "angular",
                    IsActive = true,
                    PublishedOn = DateTime.Now.AddDays(-40),
                    Tags = context.Tags.Take(4).ToList(),
                    Image = "3.jpg",
                    UserId = 1
                    
                },
                new Post
                {
                    Title = "Web Tasarım",
                    Content = "Web tasarım hakkında bilgiler",
                    Url = "web-tasarim",
                    IsActive = true,
                    PublishedOn = DateTime.Now.AddDays(-50),
                    Tags = context.Tags.Take(4).ToList(),
                    Image = "3.jpg",
                    UserId = 1
                });
        context.SaveChanges();
    }
}