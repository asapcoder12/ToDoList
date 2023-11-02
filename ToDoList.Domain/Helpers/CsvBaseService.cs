using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using ToDoList.Domain.ViewModels.Task;

namespace ToDoList.Domain.Utils
{
    public class CSVBaseService<T>
    {
        private readonly CsvConfiguration _csvConfiguration;
        public CSVBaseService()
        {
            _csvConfiguration = GetConfiguration();
        }

        public byte[] UploadFile(IEnumerable<T> data)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWtiter = new CsvWriter(streamWriter, _csvConfiguration))
            {
                csvWtiter.WriteRecords(data);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }

        public CsvConfiguration GetConfiguration()
        {
            return new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.UTF8,
                NewLine = "\r\n"
            };
        }
    }
}
