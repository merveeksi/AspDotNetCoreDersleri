using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    private IPostRepository _postRepository;
    private ICommentRepository _commentRepository;
    private ITagRepository _tagRepository;
    
    //Inject yöntemiyle nesne oluşturma
    public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _tagRepository = tagRepository;
    }
    
    public async Task<IActionResult> Index(string tag)
    {
        var posts = _postRepository.Posts.Where(i => i.IsActive == true); //aktif olan postları getir
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
                .Include(x => x.User) //yazarı getir //post'un user'ını getir
                .Include(x => x.Tags) //tag'leri getir
                .Include(x=>x.Comments) //yorumları getir
                .ThenInclude(x=>x.User) //yorumun kullanıcısını getir
                .FirstOrDefaultAsync(p => p.Url == url));
        
    }
    
    [HttpPost]
    public JsonResult AddComment(int PostId, string Text)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.FindFirstValue(ClaimTypes.Name);
        var avatar = User.FindFirstValue(ClaimTypes.UserData);
        
        var entity = new Comment
        {
            PostId = PostId,
            UserId = int.Parse(userId ?? ""),
            Text = Text,
            PublishedOn = DateTime.Now
        };
        _commentRepository.CreateComment(entity);

        return Json(new
        {
            username,
            Text,
            entity.PublishedOn,
            avatar
        });
    }
    
    [Authorize] //sadece giriş yapmış kullanıcılar erişebilir
    public IActionResult Create() //yeni post oluşturma
    {
        return View();
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Create(PostCreateViewModel model) //yeni post oluşturma
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            _postRepository.CreatePost(new Post
            {
                Title = model.Title,
                Content = model.Content,
                Url = model.Url,
                UserId = int.Parse(userId ?? ""),
                PublishedOn = DateTime.Now,
                Image = "1.jpg",
                IsActive = false
            });
            return RedirectToAction("Index");
        }
        return View(model);
    }
    
    [Authorize] 
    public async Task<IActionResult> List() //yazarın yazılarını listeleme
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
        var role = User.FindFirstValue(ClaimTypes.Role);

        var posts = _postRepository.Posts;
        
        if(string.IsNullOrEmpty(role)) //rolü var mı kontrol et
        {
            posts = posts.Where(i => i.UserId == userId);
        }
        return View(await posts.ToListAsync());
    }
    
    [Authorize]
    public IActionResult Edit(int? id) //post düzenleme
    {
        if(id == null)
        {
            return NotFound();
        }
        var post = _postRepository.Posts.Include(i => i.Tags).FirstOrDefault(i => i.PostId== id); //veritabanından postu getir
        if(post == null)
        {
            return NotFound(); //post yoksa 404 hatası ver
        }
        ViewBag.Tags = _tagRepository.Tags.ToList(); //viewbag aracılığıyla sayfaya gönderiyoruz
        return View(new PostCreateViewModel
        {
            PostId = post.PostId,
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
            Url = post.Url,
            IsActive = post.IsActive,
            Tags = post.Tags
        });
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Edit(PostCreateViewModel model) //post düzenleme
    {
        if (ModelState.IsValid)
        {
            var entityToUpdate = new Post
            {
                PostId = model.PostId,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                Url = model.Url
            };

            if (User.FindFirstValue(ClaimTypes.Role) == "admin")
            {
                entityToUpdate.IsActive = model.IsActive;
            }
            
            _postRepository.EditPost(entityToUpdate);
            return RedirectToAction("List");
        }
        return View(model);
    }
}