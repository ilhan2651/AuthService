using Entity.Concrete;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(UserRegisterDto dto);
        Task<AuthResponseDto> LoginUserAsync(UserLoginDto dto);
        Task<User> GetUserByIdAsync(int id);
        Task<AuthResponseDto> RefreshTokenAsync(string email, string refreshToken);
        Task LogoutUserAsync(string email);
        Task UpdateUserAsync(UserUpdateDto dto);


    }
}
