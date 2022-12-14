using UserRegistrationMvc.Models;
using UserRegistrationMvc.ViewModels;

namespace UserRegistrationMvc.Services
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterVM registerVM);
        Task<bool> Login(UserLoginVM loginVM);
        Task<List<User>> GetUsers();
        Task<User> GetUserAsync(UserLoginVM loginVM);
    }
}
