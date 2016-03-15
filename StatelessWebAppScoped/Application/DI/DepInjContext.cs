using System;
using System.Composition;
using System.Web;
using System.Web.SessionState;
using DepInj.Factory;

namespace StatelessWebAppScoped.Application.DI {
    public static class DepInjContext {
        private static readonly HttpApplicationEventHandlers _httpApplicationEventHandlers = new HttpApplicationEventHandlers();

        // ReSharper disable once InconsistentNaming
        private const string SessionKeyCPE = "Session.ScopeCompositionContext";

        // ReSharper disable once InconsistentNaming
        private const string ContextItemKeyRequestCPE = "Request.ScopeCompositionContext";
        // ReSharper disable once InconsistentNaming
        private const string ContextItemKeySessionBasedRequestCPE = "SessionBasedRequest.ScopeCompositionContext";

        // Helper
        private static HttpContext Context => HttpContext.Current;

        private static bool HasSession => HttpContext.Current.Session != null;
        private static HttpSessionState Session => HttpContext.Current.Session;

        private static Export<CompositionContext> SessionScopeCompositionContextExport {
            get { return HasSession ? Session[SessionKeyCPE] as Export<CompositionContext> : null; }
            // throw exception is session is not initialized
            set { Session[SessionKeyCPE] = value; }
        }

        private static Export<CompositionContext> RequestScopeCompositionContextExport {
            get { return Context.Items[ContextItemKeyRequestCPE] as Export<CompositionContext>; }
            set { Context.Items[ContextItemKeyRequestCPE] = value; }
        }

        private static Export<CompositionContext> SessionBasedRequestScopeCompositionContextExport {
            get { return Context.Items[ContextItemKeySessionBasedRequestCPE] as Export<CompositionContext>; }
            set { Context.Items[ContextItemKeySessionBasedRequestCPE] = value; }
        }

        // DepInj.DI instance
        private static readonly DepInj.DI Di = new DepInj.DI(DepInj.DI.AssembliesInDirectory(HttpRuntime.BinDirectory));

        /// <summary>
        /// Application scope composition context
        /// </summary>
        public static CompositionContext AppScopeCompositionContext => Di.GetContainer();

        /// <summary>
        /// Session scope composition context
        /// </summary>
        public static CompositionContext SessionScopeCompositionContext => SessionScopeCompositionContextExport?.Value;

        /// <summary>
        /// Request scope composition context
        /// </summary>
        public static CompositionContext RequestScopeCompositionContext => RequestScopeCompositionContextExport?.Value;

        /// <summary>
        /// Session based Request scope composition context
        /// </summary>
        public static CompositionContext SessionBasedRequestScopeCompositionContext => SessionBasedRequestScopeCompositionContextExport?.Value;

#region Session scope

        private static void CreateSessionScope() {
            if (SessionScopeCompositionContextExport == null) {
                SessionScopeCompositionContextExport = AppScopeCompositionContext.GetExport<SessionCompositionExportProviderFactory>()
                                                                             .GetFactory()
                                                                             .CreateExport();
            }
        }

        private static void DisposeSessionScope() {
            if (SessionScopeCompositionContextExport == null) {
                throw new Exception("Session scope export is not set");
            }

            SessionScopeCompositionContextExport?.Dispose();
            SessionScopeCompositionContextExport = null;
        }

#endregion

#region Session based request scope

        private static void CreateSessionBasedRequestScope() {
            if (SessionBasedRequestScopeCompositionContextExport != null) {
                throw new Exception("Must be called once per request");
            }

            if (SessionScopeCompositionContext == null) {
                throw new Exception("Session scope must be initialized before session based request scope.");
            }

            SessionBasedRequestScopeCompositionContextExport = SessionScopeCompositionContext
                    .GetExport<SessionBasedRequestCompositionExportProviderFactory>()
                    .GetFactory()
                    .CreateExport();
        }

        private static void DisposeSessionBasedRequestScope() {
            if (SessionBasedRequestScopeCompositionContextExport == null) {
                throw new Exception("Session based request scope export is not set");
            }

            SessionBasedRequestScopeCompositionContextExport.Dispose();
            SessionBasedRequestScopeCompositionContextExport = null;
        }

#endregion

#region Request scope

        private static void CreateRequestScope() {
            if (RequestScopeCompositionContextExport != null) {
                throw new Exception("Must be called once per request");
            }

            RequestScopeCompositionContextExport = AppScopeCompositionContext
                    .GetExport<RequestCompositionExportProviderFactory>()
                    .GetFactory()
                    .CreateExport();
        }

        private static void DisposeRequestScope() {
            if (RequestScopeCompositionContextExport == null) {
                throw new Exception("Request scope export is not set");
            }

            RequestScopeCompositionContextExport.Dispose();
            RequestScopeCompositionContextExport = null;
        }

        #endregion

#region Event handlers

        public static void Init(HttpApplication application) {
            _httpApplicationEventHandlers.InitEvents(application);
        }

        internal class HttpApplicationEventHandlers {
            internal void InitEvents(HttpApplication application) {
                var session = application.Modules["Session"] as SessionStateModule;
                if (session != null) {
                    session.Start += Session_Start;
                    session.End += Session_End;
                }

                application.BeginRequest += Application_BeginRequest;
                application.EndRequest += Application_EndRequest;
                application.AcquireRequestState += Application_AcquireRequestState;
            }

#region HttpApplication event handlers

            private void Session_Start(object sender, EventArgs e) {
                if (!HasSession) {
                    return;
                }

                CreateSessionScope();

                // Initialize session based request if it is not already initialized
                if (SessionBasedRequestScopeCompositionContextExport == null) {
                    CreateSessionBasedRequestScope();
                }
            }

            private void Session_End(object sender, EventArgs e) {
                if (!HasSession) {
                    return;
                }

                DisposeSessionScope();

                // Dispose session based request if not already done
                if (SessionBasedRequestScopeCompositionContextExport != null) {
                    DisposeSessionBasedRequestScope();
                }
            }

            private void Application_AcquireRequestState(object sender, EventArgs e) {
                if (!HasSession) {
                    return;
                }
                // If session is initialized already setup the session based request
                if (SessionBasedRequestScopeCompositionContextExport == null &&
                    SessionScopeCompositionContext != null) {
                    CreateSessionBasedRequestScope();
                }
            }

            private void Application_BeginRequest(object sender, EventArgs e) {
                CreateRequestScope();
            }

            private void Application_EndRequest(object sender, EventArgs e) {
                DisposeRequestScope();

                // Dispose session based request if not already done
                if (SessionBasedRequestScopeCompositionContextExport != null) {
                    DisposeSessionBasedRequestScope();
                }
            }

#endregion

        }

#endregion

    }
}