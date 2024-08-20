using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models; 

public class LoginViewModel
{
    [Required] //zorunlu alan
    [EmailAddress] //email formatında olmalı
    [Display(Name="Email Adresiniz")] //görünen isim
    
    public string? Email { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)] //şifre en az 6 en fazla 10 karakter olmalı
    [DataType(DataType.Password)] //şifre tipinde olmalı
    [Display(Name="Şifreniz")]
    
    public string Password { get; set; }
}