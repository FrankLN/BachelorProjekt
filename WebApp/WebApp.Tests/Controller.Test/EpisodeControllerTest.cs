using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;


namespace WebApp.Tests.Controller.Test
{
    [TestClass]
    public class EpisodeControllerTest
    {
        [TestMethod]
        public void IndexViewTest()
        {
            var episodeController = new WebApp.Controllers.EpisodeController();

            var result = episodeController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexViewTestWithArguments()
        {
            var episodeController = new WebApp.Controllers.EpisodeController();

            var result = episodeController.Index("20150918153452", "20150918153452") as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
