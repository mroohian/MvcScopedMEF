using System;
using System.Composition;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StatelessWebAppScoped.Controllers.Core;

namespace StatelessWebAppScoped.Application.DI {
    // ReSharper disable once ClassNeverInstantiated.Global
    [Export(typeof(IControllerFactory))]
    [Shared]
    public class CompositionControllerFactory : DefaultControllerFactory {
        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// 
        /// <returns>
        /// The controller instance.
        /// </returns>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param><param name="controllerType">The type of the controller.</param><exception cref="T:System.Web.HttpException"><paramref name="controllerType"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="controllerType"/> cannot be assigned.</exception><exception cref="T:System.InvalidOperationException">An instance of <paramref name="controllerType"/> cannot be created.</exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            var compositionContext = DepInjContext.SessionBasedRequestScopeCompositionContext ??
                                     DepInjContext.RequestScopeCompositionContext;

            var export = compositionContext?.GetExport(controllerType);

            if (export == null) {
                throw new HttpException((int)HttpStatusCode.NotFound, "Controller not found.");
            }

            HttpContext.Current.Items[typeof(MyBaseController)] = export;

            return export as IController;
        }
    }
}