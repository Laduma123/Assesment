using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataTransferObject;
using BusinessLogicLayer;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //specify the location of the csv file
            string fileName = @"C:\Users\SNanti\Documents\Projects\Outsurance\Specifications\data.csv";
            // Act
            BLL bll = new BLL();
            IEnumerable<Customer> customers = bll.ReadCSV(fileName); 
            //this tests if returned customers collection is of type IEnumerable collection of customers
            Assert.IsInstanceOfType(customers, typeof(IEnumerable<Customer>));
        }
    }
}