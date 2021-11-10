using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BooksManager.Data
{
    public class BooksManager
    {
        public async Task<IEnumerable<Volumeinfo>> GetVolumeinfoFromGoogleAsync(string search)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={search}";

            var http = new HttpClient();

            var json = await http.GetStringAsync(url);

            BooksResult br = JsonSerializer.Deserialize<BooksResult>(json);

            return br.items.Select(x => x.volumeInfo);
        }

        public async Task Save(string fileName, IEnumerable<Volumeinfo> volumeinfo)
        {
            var opts = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize(volumeinfo, opts);
                       
            await File.WriteAllTextAsync(fileName, json);
        }

        public async Task<IEnumerable<Volumeinfo>> Load(string fileName)
        {
            var json = await File.ReadAllTextAsync(fileName);
            return JsonSerializer.Deserialize<IEnumerable<Volumeinfo>>(json);
        }


    }
}
