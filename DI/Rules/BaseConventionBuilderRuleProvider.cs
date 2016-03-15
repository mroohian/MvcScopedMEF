using System.Composition.Convention;
using DepInj.Contract;
using DepInj.Contract.Attributes;
using DepInj.Factory;

namespace DepInj.Rules {
    [ConventionBuilderRuleProvider]
    internal sealed class BaseConventionBuilderRuleProvider : IConventionBuilderRuleProvider {
        public void AddRules(ConventionBuilder conventionBuilder) {
            conventionBuilder.ForType<SessionCompositionExportProviderFactory>()
                             .Export()
                             .Shared();

            conventionBuilder.ForType<RequestCompositionExportProviderFactory>()
                             .Export()
                             .Shared();

            conventionBuilder.ForType<SessionBasedRequestCompositionExportProviderFactory>()
                             .Export()
                             .Shared(MefBoundaries.Session);
        }
    }
}
