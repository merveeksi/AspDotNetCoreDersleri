using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.ViewComponents;

public class TagsMenu: ViewComponent
{
    private ITagRepository _tagRepository;

    public TagsMenu(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public IViewComponentResult Invoke()
    {
        return View(_tagRepository.Tags.ToList());
    }
}