using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiereManager.Model
{
    public interface IRepository
    {
        int Count<T>() where T : class;

        IEnumerable<T> GetAll<T>() where T : class;
        void SaveAll<T>(IEnumerable<T> zeug) where T : class;
    }
}
