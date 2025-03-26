using Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public DisabilityType DisabilityType { get; set; }
        public string PasswordHash { get; set; }
        public string? RefreshToken { get; set; } // Refresh Token saklama
        public DateTime? RefreshTokenExpiry { get; set; } // Refresh Token süresi
    }
}
