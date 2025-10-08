// using API.DTOs;

// namespace API.Services.Interface
// {
//     public interface IAuthenticationRepo
//     {
//         Task<AuthenticationDTO> BuildToken(UserCredentialsDTO userCredentialsDTO);
//     }
// }
using API.DTOs;
using System.Threading.Tasks;

namespace API.Services.Interface
{
    public interface IAuthenticationRepo
    {
        Task<AuthenticationDTO> BuildToken(UserCredentialsDTO userCredentialsDTO);
    }
}
