using AutoVenREST.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private LoggingController con = new LoggingController();


        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<Logging> get = con.Get();

             Assert.IsTrue(get.Count() > 0);

        }

        [TestMethod]
        [Priority(0)]
        public void PostandDeleteTest()
        {

            var d = con.Get().Count();

            con.Post(new Logging(Convert.ToDateTime("2019-11-26T00:00:00"), 200.3, false));

            var e = con.Get().Count();

            //Post
            Assert.AreEqual(d , e-1);

            IEnumerable<Logging> log = con.Get();

            con.Delete(log.Last().Id);

            //Delete
            Assert.AreEqual(d, con.Get().Count());
        }

        [TestMethod]
        public void TestExpiredEntry()
        {
            IEnumerable<Logging> list = con.Get();
            int lengthInit = list.Count();

            DateTime date = DateTime.Now - TimeSpan.FromDays(1830);
            Logging log = new Logging(date, 50, false);
            con.Post(log);

            IEnumerable<Logging> listActual = con.Get();
            int lengthAct = listActual.Count();

            Assert.AreNotSame(lengthInit, lengthAct);

            con.Post(log);
            int newLength = listActual.Count();

            Assert.AreEqual(lengthAct, newLength);

            listActual = con.Get();
            con.Delete(listActual.Last().Id);
        }
    }
}
