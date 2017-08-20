using DataAccessLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLL
    {
        public IEnumerable<Customer> ReadCSV(string fileName)
        {
            DAL dal = new DAL();
            return dal.ReadCSV(fileName);
        }
    }
}
