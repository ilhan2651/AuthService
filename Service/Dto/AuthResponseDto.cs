using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class AuthResponseDto
    {
        public string Token { get; set; } // JWT Token
        public string RefreshToken { get; set; } // Yenileme Token'ı
    }
}
