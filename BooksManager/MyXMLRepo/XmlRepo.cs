using BooksManager;
using BooksManager.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyXMLRepo
{
    public class XmlRepo : IBooksRepository
    {
        public XmlRepo(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }

        public Task<IEnumerable<Volumeinfo>> Load()
        {
            return Task.Run(() =>
            {
                using (var sr = new StreamReader(FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    return (IEnumerable<Volumeinfo>)serial.Deserialize(sr);
                }
            });
        }

        public Task Save(IEnumerable<Volumeinfo> volumeinfo)
        {
            return Task.Run(() =>
            {
                using (var sw = new StreamWriter(FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    serial.Serialize(sw, volumeinfo.ToList());
                }
            });
        }
    }
}
