using API.DTOs;

namespace API.Services.Interface
{
    public interface IAuthenticationRepo
    {
        Task<AuthenticationDTO> BuildToken(UserCredentialsDTO userCredentialsDTO);
    }
}