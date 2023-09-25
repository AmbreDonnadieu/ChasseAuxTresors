using ChasseAuxTresorsApi.Interfaces;

namespace ChasseAuxTresorsApi.Services
{
    public class FileService : IFileService
    {
        public FileService() { }

        public async Task<List<string>> ConvertFileToListStringAsync(IFormFile file)
        {
            var allLines = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    allLines.Append(await reader.ReadLineAsync());
                }
            }
            return allLines;
        }
    }
}
