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
                string? line;
                while (!string.IsNullOrEmpty(line = await reader.ReadLineAsync()))
                {
                    allLines.Add(line);
                }
            }
            return allLines;
        }
    }
}
