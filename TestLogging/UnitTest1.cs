using AutoVenREST.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLib;
using System;
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
        public void PostandDeleteTest()
        {

            var d = con.Get().Count();

            con.Post(new Logging(Convert.ToDateTime("2019-11-26T00:00:00"), 200.3));

            var e = con.Get().Count();

            //Post
            Assert.AreEqual(d , e-1);

            IEnumerable<Logging> log = con.Get();

            con.Delete(log.Last().Id);

            //Delete
            Assert.AreEqual(d, con.Get().Count());
        }
    }
}
