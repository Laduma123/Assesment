using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LINQtoCSV;
using BusinessLogicLayer;
using DataTransferObject;

namespace Assesment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {

            //start processing 
            try
            {
                //instantiate object of CSVFileDescription class from LINQToCSV library
                CsvFileDescription outputFileDescription = new CsvFileDescription
                {
                    SeparatorChar = ',', // tab delimited
                    FirstLineHasColumnNames = true, //means the first row contains headers in the  
                   
                };

                //instantiate object of CSVContext class
                CsvContext cc = new CsvContext();
                //object of BLL class
                BLL bll = new BLL();
                //invoke ReadCSV method and assign the results to IEnumerable collection of customers
                IEnumerable<Customer> customers = bll.ReadCSV(openFileDialog1.FileName);
                
                //collection of customers by first name
                var customersByName =
                 from c in customers
                 //sort by first name
                 orderby c.FirstName
                 //group all the first names
                 group c by c.FirstName into customerGroup
                 select new
                 {
                     Name = customerGroup.Key,
                     Frequency = customerGroup.Count(),
                 };

                //process addresses
                int number;
                List<Address> addresses = customers.Select(s => new
                {
                    FullAddress = s.Address.Trim(),
                    Tokens = s.Address.Split()
                })
                .Select(x => new Address
                {
                    FullAddress = x.FullAddress,
                    Street = String.Join(" ", x.Tokens.Skip(1)),
                    Number = int.TryParse(x.Tokens[0], out number) ? number : int.MaxValue
                })
                .OrderBy(addr => addr.Number)
                .ToList();

                outputFileDescription.EnforceCsvColumnAttribute = true;
                //write names to csv ordered by frequency and name
                cc.Write(customersByName.OrderByDescending(x => x.Frequency).OrderBy(x => x.Name), "names.csv");
                //write addresses to a csv ordered by street name
                cc.Write(addresses.OrderBy(x => x.Street), "addresses.csv");
                //if everything goes well, display success message
                MessageBox.Show("CSV files generated succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch(Exception ex)
            {
                //catch any exception and display appropriate error message
                MessageBox.Show(ex.Message, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    
}
