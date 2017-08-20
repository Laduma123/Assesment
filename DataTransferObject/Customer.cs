using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class Customer
    {
        [CsvColumn(Name = "FirstName", FieldIndex = 1)]
        public string FirstName { get; set; }
        [CsvColumn(Name = "LastName", FieldIndex = 2)]
        public string LastName { get; set; }
        [CsvColumn(Name = "Address", FieldIndex = 3)]
        public string Address { get; set; }
        [CsvColumn(Name = "PhoneNumber", FieldIndex = 4)]
        public string PhoneNumber { get; set; } 
    }
}
