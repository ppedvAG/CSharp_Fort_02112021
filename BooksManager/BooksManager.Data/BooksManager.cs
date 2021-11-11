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

        public IBooksRepository BooksRepository { get;  }

        public BooksManager(IBooksRepository booksRepository)
        {
            BooksRepository = booksRepository;
        }


        public Volumeinfo GetBookWithMostPages()
        {
            return BooksRepository.Load().Result.OrderBy(x => x.pageCount).FirstOrDefault();
        }

        public async Task<IEnumerable<Volumeinfo>> GetVolumeinfoFromGoogleAsync(string search)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={search}";

            var http = new HttpClient();

            var json = await http.GetStringAsync(url);

            BooksResult br = JsonSerializer.Deserialize<BooksResult>(json);

            return br.items.Select(x => x.volumeInfo);
        }



    }
}
