using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests.ViewModel.Test
{
    [TestClass]
    public class EpisodeViewModelTest
    {
        [TestMethod]
        public void EpisodeViewModel_Calc_0()
        {
            var episodeViewModel = new ViewModels.EpisodeViewModel("Test", 0, 100);

            int actual = episodeViewModel.ProcentTransmission;
            int expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EpisodeViewModel_Calc_100()
        {
            var episodeViewModel = new ViewModels.EpisodeViewModel("Test", 100, 100);

            int actual = episodeViewModel.ProcentTransmission;
            int expected = 100;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EpisodeViewModel_Calc_42()
        {
            var episodeViewModel = new ViewModels.EpisodeViewModel("Test", 42, 99); // 42 / 99 * 100 = 42,42 which should be rounded down to 42

            int actual = episodeViewModel.ProcentTransmission;
            int expected = 42;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EpisodeViewModel_Calc_43()
        {
            var episodeViewModel = new ViewModels.EpisodeViewModel("Test", 42, 98); // 42 / 98 * 100 = 42,857 which should be rounded up to 43

            int actual = episodeViewModel.ProcentTransmission;
            int expected = 43;

            Assert.AreEqual(expected, actual);
        }
    }
}
