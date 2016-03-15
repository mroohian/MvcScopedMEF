using System.Composition;
using System.Web.Mvc;
using DepInj.Contract;
using Domain.Contract;
using Domain.Contract.Core;
using StatelessWebAppScoped.Controllers.Core;

namespace StatelessWebAppScoped.Controllers {
    [Export]
    public class HomeController : MyBaseController {
        private readonly ICore _core;
        private readonly ISessionContext _sessionContext;
        private readonly ISettingDomain _settingDomain1;
        private readonly ISettingDomain _settingDomain2;
        private readonly IArticleDomain _articleDomain1;
        private readonly IArticleDomain _articleDomain2;

        [ImportingConstructor]
        public HomeController(ICore core, ISessionContext sessionContext, ISettingDomain settingDomain1, ISettingDomain settingDomain2, IArticleDomain articleDomain1, IArticleDomain articleDomain2) {
            _core = core;
            _sessionContext = sessionContext;
            _settingDomain1 = settingDomain1;
            _settingDomain2 = settingDomain2;
            _articleDomain1 = articleDomain1;
            _articleDomain2 = articleDomain2;

            _core.AddDebugLines("Core", DebugLines);

            DebugLines["ISessionContext"] = _sessionContext.GetHashCode().ToString();
            _sessionContext.AddDebugLines("ISessionContext", DebugLines);

            DebugLines["_SP1"] = "";

            DebugLines["ISettingDomain1"] = _settingDomain1.GetHashCode().ToString();
            _settingDomain1.AddDebugLines("ISettingDomain1", DebugLines);

            DebugLines["_SP2"] = "";

            DebugLines["ISettingDomain2"] = _settingDomain2.GetHashCode().ToString();
            _settingDomain2.AddDebugLines("ISettingDomain2", DebugLines);

            DebugLines["_SP3"] = "";

            DebugLines["IArticleDomain1"] = _articleDomain1.GetHashCode().ToString();
            _articleDomain1.AddDebugLines("IArticleDomain1", DebugLines);

            DebugLines["_SP4"] = "";

            DebugLines["IArticleDomain2"] = _articleDomain2.GetHashCode().ToString();
            _articleDomain2.AddDebugLines("IArticleDomain2", DebugLines);
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}