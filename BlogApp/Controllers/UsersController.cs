using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    
    //enject ettik
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Login()
    {
        if(User.Identity.IsAuthenticated) //kullanıcı giriş yapmışsa
        {
            return RedirectToAction("Index", "Posts"); //anasayfaya yönlendir
        }
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //cookie temizle
        return RedirectToAction("Login");
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
           var isUser = await _userRepository.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password); //kullanıcı var mı kontrolü
           if (isUser != null) //kullanıcı varsa
           {
               var userClaims = new List<Claim>();
               
               userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString())); //kullanıcı id bilgisi alındı ve claim oluşturuldu
               userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? "")); //kullanıcı adı bilgisi alındı
               userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? "")); 
                
               if(isUser.Email == "merve@gmail.com")
               {
                   userClaims.Add(new Claim(ClaimTypes.Role, "admin")); //admin rolü verildi
               }
               
               var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme); 
               
                var authProperties = new AuthenticationProperties
                {
                     IsPersistent = true //beni hatırla seçeneği
                };
                
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //çıkış yap
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties); //giriş yap
                
                return RedirectToAction("Index", "Posts"); //anasayfaya yönlendir
           }
           else
           {
               ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
           }
        }
       
        return View(model);
    }
}
