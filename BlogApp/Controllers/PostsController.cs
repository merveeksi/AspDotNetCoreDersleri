using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    private IPostRepository _postRepository;
    private ITagRepository _tagRepository;
    
    //Inject yöntemiyle nesne oluşturma
    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public IActionResult Index()
    {
        return View(new PostsViewModel
        {
            Posts = _postRepository.Posts.ToList()
        });
    }
}