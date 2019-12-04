using System;

using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Edge;

using OpenQA.Selenium.Firefox;

using OpenQA.Selenium.IE;



// NuGet packages must be updated to 3.141



namespace UnitTest

{

    [TestClass]

    public class UITest

    {

        private static readonly string DriverDirectory = "C:\\Users\\PC\\Desktop\\driverTest";

        private static IWebDriver _driver;



        [ClassInitialize]

        public static void Setup(TestContext context)

        {
            _driver = new ChromeDriver(DriverDirectory); // fast

        }



        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }



        [TestMethod]
        public void Login()
        {
            string URI = "http://localhost:3000/index.htm";
            string usernameFelt = "email";
            string passwordFelt = "kodeord";
            string logIndKnap = "buttonLogin";
            string outputFelt = "logindValidation";

            // Finder første frem til hjemmesiden
            _driver.Navigate().GoToUrl(URI);
            // Vi ser om den kan finde frem til den
            Assert.AreEqual("AutoVen Log ind", _driver.Title);

            

            // Vi skriver username til elementet
            IWebElement usernameElement = _driver.FindElement(By.Id(usernameFelt));
            usernameElement.SendKeys("Admin");

            // Vi skriver password til elementet
            IWebElement passwordElement = _driver.FindElement(By.Id(passwordFelt));
            passwordElement.SendKeys("Admin");

            // Vi trykke på log ind knappen
            IWebElement buttonElement = _driver.FindElement(By.Id(logIndKnap));
            buttonElement.Click();

            // Her skal den have været gået til næste side! Så er koden lavet korrekt
            Thread.Sleep(1000);
            Assert.AreEqual("AutoVen", _driver.Title);
            
        }

    }

}