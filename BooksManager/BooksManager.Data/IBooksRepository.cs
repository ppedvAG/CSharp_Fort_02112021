using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksManager.Data
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Volumeinfo>> Load();
        Task Save(IEnumerable<Volumeinfo> volumeinfo);
    }
}