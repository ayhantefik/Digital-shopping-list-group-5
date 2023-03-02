using Microsoft.VisualStudio.TestTools.UnitTesting;
using Digital_shopping_list_group_5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Digital_shopping_list_group_5.Tests
{
    [TestClass()]
    public class ConsumerTests
    {

        [TestMethod()]
        public void ConsumerConstructorTest()
        {
            List<int> idsOfPurchases = new List<int> { 0, 1, 2 };
            List<int> idsOfReceipts = new List<int> { 10, 11, 12 };
            Consumer consumer = new Consumer("email","qwerty","Emilia",0,0,idsOfPurchases,idsOfReceipts);

            Assert.IsNotNull(consumer);
            Assert.AreEqual(consumer.Email, "email");
            Assert.AreEqual(consumer.Password, "qwerty");
            Assert.AreEqual(consumer.Name, "Emilia");
            Assert.AreEqual(consumer.AccountLvl, 0);
            Assert.AreEqual(consumer.Points, 0);
            Assert.AreEqual(consumer.IdsOfPurchaseLists, idsOfPurchases);
            Assert.AreEqual(consumer.ListOfReceipts, idsOfReceipts);

        }




        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RunSecuritySystemTest()
        {            
            Database db = new Database();
            db.LoadAllFromDatabase();
            Consumer consumer = new Consumer();

            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            StringReader sr = new StringReader("1");
            Console.SetIn(sr);

            consumer.RunSecuritySystem(db);

            string result = sw.ToString();
            string expected = "[1] Login\n\r[2] Registration\n\r1\n\rYour email: ";
            Assert.AreEqual(expected, result);
        }
    }
}