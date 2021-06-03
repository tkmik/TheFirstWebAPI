using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPILibrary.Models;

namespace WebAPILibrary.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        Task<IEnumerable<User>> GetUsersAsync();
        Task AddUserAsync(User user);
    }
}
