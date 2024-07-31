using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Models;


public class Product
{
    [Display(Name="Urun Id")] 
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Boş Geçilemez")] //zorunlu alan
    [Display(Name = "Urun Adı")]
    public string Name { get; set; } 
    
    [Required]
    [Range(0,300000)]
    [Display(Name = "Fiyat")]
    public decimal? Price { get; set; } //parasal işlemlerde decimal kullanmak daha iyi
    
    [Required]
    [Display(Name = "Resim")]
    public string Image { get; set; } = string.Empty; // ? = string.Empty

    public bool IsActive { get; set; }
        [Display(Name = "Category")]
        
    [Required]
    public int? CategoryId { get; set; }
}