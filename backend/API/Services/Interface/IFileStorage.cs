namespace API.Services.Interface
{
    public interface IFileStorage
    {
        // Task <Task> DeleteFile(string fileRoute, string containerName); // Local storag
         Task DeleteFile(string fileRoute, string containerName); // for azure
        Task<string> SaveFile(string containerName, IFormFile file);
        Task<string> UpdateFile(string containerName, IFormFile file, string fileRoute); 
    }
}