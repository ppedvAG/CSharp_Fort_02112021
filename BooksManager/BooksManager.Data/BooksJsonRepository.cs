using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BooksManager.Data
{
    public class BooksJsonRepository : IBooksRepository
    {
        public string FileName { get; }

        public BooksJsonRepository(string fileName)
        {
            FileName = fileName;
        }

        public async Task Save(IEnumerable<Volumeinfo> volumeinfo)
        {
            var opts = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize(volumeinfo, opts);

            await File.WriteAllTextAsync(FileName, json);
        }

        public async Task<IEnumerable<Volumeinfo>> Load()
        {
            var json = await File.ReadAllTextAsync(FileName);
            return JsonSerializer.Deserialize<IEnumerable<Volumeinfo>>(json);
        }

    }
}
