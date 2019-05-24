using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Gui.Services
{
    /// <summary>
    /// Load in data for processing from a stream
    /// </summary>
    public abstract class LoadDataService
    {
        public IEnumerable<T> LoadFromStream<T>(Stream dataStream) where T : class
        {
            using (var sr = new StreamReader(dataStream))
            using (var cs = new CsvReader(sr))
            {
                return cs.GetRecords<T>();
            }
        }
    }
}
