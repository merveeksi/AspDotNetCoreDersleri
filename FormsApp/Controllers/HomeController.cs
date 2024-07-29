using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
   
    public HomeController()
    {
    }

    public IActionResult Index(string searchString)
    {
        var Products = Repository.Products;
        if (!string.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
        }
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
