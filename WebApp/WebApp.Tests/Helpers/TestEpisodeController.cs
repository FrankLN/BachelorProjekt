using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Tests.Helpers
{
    class TestEpisodeController : WebApp.Controllers.EpisodeController
    {
        public List<Models.DbModel> getFromDB()
        {
            return base.getFromDB();
        }
    }
}
