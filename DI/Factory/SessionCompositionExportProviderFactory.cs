using System.Composition;
using DepInj.Contract;

namespace DepInj.Factory {
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class SessionCompositionExportProviderFactory {
        private readonly ExportFactory<CompositionContext> _compositionExportProviderFactory;

        public SessionCompositionExportProviderFactory(
        [SharingBoundary(MefBoundaries.Session)]
        ExportFactory<CompositionContext> compositionExportProviderFactory) {
            _compositionExportProviderFactory = compositionExportProviderFactory;
        }

        public ExportFactory<CompositionContext> GetFactory() {
            return _compositionExportProviderFactory;
        }
    }
}