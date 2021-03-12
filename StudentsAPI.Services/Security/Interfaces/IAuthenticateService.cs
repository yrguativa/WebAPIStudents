using StudentsAPI.Models.Security;
using System.Threading.Tasks;

namespace StudentsAPI.Services.Security.Interfaces
{
    public interface IAuthenticateService
    {
        Task<TokenModel> Register(UserModel user);
        Task<TokenModel> Login(UserModel user);
    }
}
