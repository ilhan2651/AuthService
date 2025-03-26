using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class RefreshTokenRequestDto
    {
        public string Email { get; set; } // Kullanıcının email adresi
        public string RefreshToken { get; set; } // Kullanıcının mevcut refresh token'ı
    }
}
