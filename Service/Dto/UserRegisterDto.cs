using Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "İsim en az 3, en fazla 50 karakter olmalıdır.")]
        public string NameSurname { get; set; }


        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [MaxLength(20, ErrorMessage = "Şifre en fazla 20 karakter olabilir.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Şifre tekrar alanı zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Engellilik durumu zorunludur.")]
        [EnumDataType(typeof(DisabilityType), ErrorMessage = "Geçerli bir engellilik türü giriniz: 'VisuallyImpired' veya 'HardHearImpired'.")]
        public DisabilityType DisabilityType { get; set; }

    }
}
