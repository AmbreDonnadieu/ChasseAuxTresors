namespace ChasseAuxTresorsApi.Interfaces
{
    public interface IFileService
    {
        Task<List<string>> ConvertFileToListStringAsync(IFormFile file);
    }
}
