using System.Composition.Convention;
using System.Web.Mvc;
using DepInj.Contract;
using DepInj.Contract.Attributes;
using Domain.Contract.Core;
using Domain.Core;

namespace StatelessWebAppScoped.Application.DI.Rules {
    [ConventionBuilderRuleProvider]
    // ReSharper disable once UnusedMember.Global
    public class MvcApplicationConventionBuilderRuleProvider : IConventionBuilderRuleProvider {
        public void AddRules(ConventionBuilder conventionBuilder) {
            /*conventionBuilder.ForType<CompositionControllerFactory>()
                             .Export<IControllerFactory>()
                             .Shared();

            conventionBuilder.ForType<SessionContext>()
                             .Export<ISessionContext>()
                             .Shared(MefBoundries.Session);

            conventionBuilder.ForTypesDerivedFrom<IBaseDomain>()
                             .Export()
                             .ExportInterfaces()
                             .Shared(MefBoundries.Request);

            conventionBuilder.ForTypesDerivedFrom<IController>()
                             .Export(); // Not interface shared, not shared */
        }
    }
}