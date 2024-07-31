using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
   
    public HomeController(List<Category> categories)
    {
        Categories = categories;
    }

    [HttpGet]
    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Products;
       
        if (!String.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p => p.Name!.ToLower().Contains(searchString)).ToList(); // ! "hata olmayacağına söz verme" ya da 'Product.cs'de name olanın karşısına "null!" getirirsin.
        }
        if (!String.IsNullOrEmpty(category) && category !="0")
        {
            products = products.Where(p => p.CategoryId ==int.Parse(category)).ToList();
        }

        // ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name", category);

        var model = new ProductViewModel 
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model);
    }

    public List<Category> Categories { get; set; }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
        var extension = Path.GetExtension(imageFile.FileName);       //uzantıyı bulma(jpg...)
        var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");  //yeni bir isim tanımlama
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

        if (imageFile != null)
        {
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Geçerli bir resim seçiniz");
            }
        }
        
        if (ModelState.IsValid)
        {
            using(var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            model.Image = randomFileName;
            model.ProductId = Repository.Products.Count + 1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }
}

