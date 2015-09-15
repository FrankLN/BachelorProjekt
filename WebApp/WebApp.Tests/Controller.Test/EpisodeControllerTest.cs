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
    }
}
