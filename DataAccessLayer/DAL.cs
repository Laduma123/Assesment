using DataTransferObject;
using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DAL
    {
        public IEnumerable<Customer> ReadCSV(string fileName)
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();
            //read file contents and return the results
            return cc.Read<Customer>(fileName, inputFileDescription);
        }
    }
}
