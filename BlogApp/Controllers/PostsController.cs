using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    private IPostRepository _postRepository;
    private ICommentRepository _commentRepository;
    
    //Inject yöntemiyle nesne oluşturma
    public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }
    
    public async Task<IActionResult> Index(string tag)
    {
        var posts = _postRepository.Posts; 
        if (!string.IsNullOrEmpty(tag)) //tag filtreleme
        {
            return View(new PostsViewModel
            {
                Posts = posts.Where(x => x.Tags.Any(t => t.Url == tag)).ToList() //her ulaştığımız postun tag'lerini kontrol ediyoruz  //any metodu ile tag'lerin içindeki url'leri kontrol ediyoruz
            });
        }
        return View(new PostsViewModel
        {
            Posts = await posts.ToListAsync() //ya bütün postları getir ya da filtreleme yap
        });
    }
    
    public async Task<IActionResult> Details (string url)
    {
        return View (await _postRepository
                .Posts
                .Include(x => x.Tags) //tag'leri getir
                .Include(x=>x.Comments) //yorumları getir
                .ThenInclude(x=>x.User) //yorumun kullanıcısını getir
                .FirstOrDefaultAsync(p => p.Url == url));
        
    }
    
    [HttpPost]
    public JsonResult AddComment(int PostId, string UserName, string Text)
    {
        var entity = new Comment
        {
            PostId = PostId,
            User = new User { UserName = UserName, Image = "avatar.jpg"},
            Text = Text,
            PublishedOn = DateTime.Now
        };
        _commentRepository.CreateComment(entity);

        return Json(new
        {
            UserName,
            Text,
            entity.PublishedOn,
            entity.User.Image
        });
    }
}