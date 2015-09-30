using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests.ViewModels.Test
{
    [TestClass]
    public class EpisodeViewModelTest
    {
        [TestMethod]
        public void EpisodeViewModel_EpisodeType_Equals_Shock()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { EpisodeType = "Shock" };

            var expected = "Shock";

            Assert.AreEqual(expected, episodeViewModel.EpisodeType);
        }

        [TestMethod]
        public void EpisodeViewModel_Transmissions_42()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { Transmissions = 42 };

            var expected = 42;

            Assert.AreEqual(expected, episodeViewModel.Transmissions);
        }

        [TestMethod]
        public void EpisodeViewModel_Transmissions_0()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { Transmissions = 0 };

            var expected = 0;

            Assert.AreEqual(expected, episodeViewModel.Transmissions);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EpisodeViewModel_Transmissions_ArgumentOutOfRangeException_Negative1()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { Transmissions = -1 };
        }

        [TestMethod]
        public void EpisodeViewModel_ProcentTransmission_Equals_42()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { ProcentTransmission = 42 };

            var expected = 42;

            Assert.AreEqual(expected, episodeViewModel.ProcentTransmission);
        }

        [TestMethod]
        public void EpisodeViewModel_ProcentTransmission_Equals_100()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { ProcentTransmission = 100 };

            var expected = 100;

            Assert.AreEqual(expected, episodeViewModel.ProcentTransmission);
        }

        [TestMethod]
        public void EpisodeViewModel_ProcentTransmission_Equals_0()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { ProcentTransmission = 0 };

            var expected = 0;

            Assert.AreEqual(expected, episodeViewModel.ProcentTransmission);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EpisodeViewModel_ProcentTransmission_ArgumentOutOfRangeException_Negative1()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { ProcentTransmission = -1 };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EpisodeViewModel_ProcentTransmission_ArgumentOutOfRangeException_101()
        {
            var episodeViewModel = new WebApp.ViewModels.EpisodeViewModel() { ProcentTransmission = 101 };
        }
    }
}
