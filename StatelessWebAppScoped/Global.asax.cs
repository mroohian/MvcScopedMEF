using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StatelessWebAppScoped.Application.DI;

namespace StatelessWebAppScoped {
    public class MvcApplication : HttpApplication {
        public override void Init() {
            DepInjContext.Init(this);
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(DepInjContext.AppScopeCompositionContext.GetExport<IControllerFactory>());
        }
    }
}
