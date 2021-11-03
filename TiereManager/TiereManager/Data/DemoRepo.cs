using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiereManager.Model;

namespace TiereManager.Data
{
    public class DemoRepo : IRepository
    {
        public int Count<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            if (typeof(T) == typeof(Katze))
            {
                var demoKatzen = new List<Katze>();
                demoKatzen.Add(new Katze());
                demoKatzen.Add(new Katze());
                demoKatzen.Add(new Katze());
                demoKatzen.Add(new Katze());
                return demoKatzen.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public void SaveAll<T>(IEnumerable<T> zeug) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
