namespace API.Services.Interface
{
    public interface IFileStorage
    {
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> SaveFile(string containerName, IFormFile file);
        Task<string> UpdateFile(string containerName, IFormFile file, string fileRoute); 
    }
}