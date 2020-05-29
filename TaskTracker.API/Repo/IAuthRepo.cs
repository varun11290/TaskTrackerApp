using System.Threading.Tasks;
using TaskTracker.API.Models;

namespace TaskTracker.API.Repo
{
    public interface IAuthRepo
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName,string password);

        Task<bool> UserExist(string userName);

    }

}