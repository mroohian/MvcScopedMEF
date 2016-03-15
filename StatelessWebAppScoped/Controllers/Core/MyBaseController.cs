using System.Collections.Generic;
using System.Web.Mvc;

namespace StatelessWebAppScoped.Controllers.Core {
    /// <summary>
    /// Base of all DI Controllers
    /// </summary>
    /// <remarks>
    /// This class is abstract and therefore MEF will not find it.
    /// </remarks>
    public abstract class MyBaseController : Controller {
        public readonly Dictionary<string, string> DebugLines;

        protected MyBaseController() {
            DebugLines = new Dictionary<string, string>();
        }


    }
}