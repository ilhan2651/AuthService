using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IUserRepository
    {
        
            Task<User> GetUserByEmailAsync(string email);
            Task<User> GetUserByIdAsync(int id);
            Task AddUserAsync(User user);
            Task UpdateUserAsync(User user);
        

    }
}
