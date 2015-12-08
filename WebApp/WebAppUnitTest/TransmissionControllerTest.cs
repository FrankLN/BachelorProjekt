using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAppUnitTest
{
    [TestClass]
    public class TransmissionControllerTest
    {
        [TestMethod]
        public void IndexReturnsIndexView()
        {
            // Arrange
            WebApp.Controllers.TransmissionController testObj = 
                new WebApp.Controllers.TransmissionController();
            var expected = "Index";
            
            // Act
            var result = testObj.Index() as ViewResult;
            var actual = result.ViewName;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

