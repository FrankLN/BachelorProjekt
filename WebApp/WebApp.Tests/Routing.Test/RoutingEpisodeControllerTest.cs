using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;

namespace WebApp.Tests.Routing.Test
{
    [TestClass]
    public class RoutingEpisodeControllerTest
    {
        #region Incoming routes
        [TestMethod]
        public void RouteWithControllerNoActionNoId()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/EpisodeController");
            var routes = new RouteCollection();
            WebApp.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("EpisodeController", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void RouteWithControllerWithActionNoId()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/EpisodeController/Index");
            var routes = new RouteCollection();
            WebApp.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("EpisodeController", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }
#endregion

        #region helpers
        static UrlHelper GetUrlHelper(string appPath = "/", RouteCollection routes = null)
        {
            if (routes == null)
            {
                routes = new RouteCollection();
                WebApp.RouteConfig.RegisterRoutes(routes);
            }

            HttpContextBase httpContext = new StubHttpContextForRouting(appPath);
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "EpisodeController");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("id", UrlParameter.Optional);
            RequestContext requestContext = new RequestContext(httpContext, routeData);
            UrlHelper helper = new UrlHelper(requestContext, routes);
            return helper;
        }
        #endregion
    }
}
