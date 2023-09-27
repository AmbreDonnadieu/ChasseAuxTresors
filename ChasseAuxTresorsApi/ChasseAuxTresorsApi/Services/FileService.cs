using ChasseAuxTresorsApi.Interfaces;

namespace ChasseAuxTresorsApi.Services
{
    public class FileService : IFileService
    {
        public FileService() { }

        public async Task<List<string>> ConvertFileToListStringAsync(IFormFile file)
        {
            //the code below was made for text file only, so we throw an error in other case
            if (Path.GetExtension(file.FileName) != ".txt")
                throw new Exception("We don't accept this type of file. Try using a text file instead.");

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
