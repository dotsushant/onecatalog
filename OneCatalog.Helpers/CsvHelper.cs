using System.Collections.Generic;
using FileHelpers;

namespace OneCatalog.Helpers
{
    public static class CsvHelper
    {
        public static IEnumerable<T> ReadRecords<T>(string fileName) where T : class
        {
            var fileReader = new FileHelperAsyncEngine<T>();

            using (fileReader.BeginReadFile(fileName))
            {
                foreach (var fileRecord in fileReader) yield return fileRecord;
            }
        }

        public static void WriteRecords<T>(string fileName, IEnumerable<T> records) where T : class
        {
            var fileWriter = new FileHelperEngine<T>();
            fileWriter.HeaderText = fileWriter.GetFileHeader();
            fileWriter.WriteFile(fileName, records);
        }
    }
}