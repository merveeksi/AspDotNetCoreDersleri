using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models; 

public class RegisterViewModel
{
    [Required]
    [Display(Name="Username")]
    public string? UserName { get; set; }
    
    [Required]
    [Display(Name="Ad Soyad")]
    public string? Name { get; set; }
    
    [Required] //zorunlu alan
    [EmailAddress] //email formatında olmalı
    [Display(Name="Email Adresiniz")] //görünen isim
    public string? Email { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)] //şifre en az 6 en fazla 10 karakter olmalı
    [DataType(DataType.Password)] //şifre tipinde olmalı
    [Display(Name="Şifreniz")]
    public string Password { get; set; }
    
    [Required]
    [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
    [DataType(DataType.Password)] //şifre tipinde olmalı
    [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")] //şifreler uyuşmuyorsa hata mesajı
    [Display(Name="Şifre Tekrarı")]
    public string ConfirmPassword { get; set; }
}