using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreApiTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApiTest.IClass;

namespace CoreApiTest.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void GetSTest()
        {
            //WeatherForecastController asw = new WeatherForecastController(new User());
            //string saf = asw.GetS("2");

        }

        [TestMethod()]
        public void GetTest()
        {
            //Assert.AreEqual("王五", new WeatherForecastController(new User()).GetS("2"));
        }
    }
}