using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests.Model.Test
{
    [TestClass]
    public class DbModelTest
    {
        [TestMethod]
        public void DbModel_EpisodeType_Equals_Shock()
        {
            var dbModel = new WebApp.Models.DbModel() { EpisodeType = "Shock" };

            var expected = "Shock";

            Assert.AreEqual(expected, dbModel.EpisodeType);
        }

        [TestMethod]
        public void DbModel_TransmissionId_Equals_42()
        {
            var dbModel = new WebApp.Models.DbModel() { TransmissionId = 42 };

            var expected = 42;

            Assert.AreEqual(expected, dbModel.TransmissionId);
        }

        [TestMethod]
        public void DbModel_TransmissionId_Equals_0()
        {
            var dbModel = new WebApp.Models.DbModel() { TransmissionId = 0 };

            var expected = 0;

            Assert.AreEqual(expected, dbModel.TransmissionId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DbModel_TransmissionId_ArgumentOutOfRangeException_Negative1()
        {
            var dbModel = new WebApp.Models.DbModel() { TransmissionId = -1 };
        }

        [TestMethod]
        public void DbModel_Date_Equals_20150930092600()
        {
            var dbModel = new WebApp.Models.DbModel() { Date = "20150930092600" };

            var expected = "20150930092600";

            Assert.AreEqual(expected, dbModel.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DbModel_Date_ArgumentOutOfRangeException_11digits()
        {
            var dbModel = new WebApp.Models.DbModel() { Date = "2015093009260" };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DbModel_Date_ArgumentOutOfRangeException_13digits()
        {
            var dbModel = new WebApp.Models.DbModel() { Date = "201509300926000" };
        }
    }
}
