using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
        
    }

    public IAsyncResult Index()
    {
        return View(Repository.Products);
    }

    public IAsyncResult Privacy()
    {
        return View();
    }
}


