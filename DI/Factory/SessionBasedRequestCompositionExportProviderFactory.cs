using System.Composition;
using DepInj.Contract;

namespace DepInj.Factory {
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class SessionBasedRequestCompositionExportProviderFactory {
        private readonly ExportFactory<CompositionContext> _compositionExportProviderFactory;

        public SessionBasedRequestCompositionExportProviderFactory(
        [SharingBoundary(MefBoundaries.Request)]
        ExportFactory<CompositionContext> compositionExportProviderFactory) {
            _compositionExportProviderFactory = compositionExportProviderFactory;
        }

        public ExportFactory<CompositionContext> GetFactory() {
            return _compositionExportProviderFactory;
        }
    }
}