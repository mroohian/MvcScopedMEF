using System.Composition;
using DepInj.Contract;

namespace DepInj.Factory {
    public sealed class RequestCompositionExportProviderFactory {
        private readonly ExportFactory<CompositionContext> _compositionExportProviderFactory;

        public RequestCompositionExportProviderFactory(
        [SharingBoundary(MefBoundaries.Request, MefBoundaries.Session)]
        ExportFactory<CompositionContext> compositionExportProviderFactory) {
            _compositionExportProviderFactory = compositionExportProviderFactory;
        }

        public ExportFactory<CompositionContext> GetFactory() {
            return _compositionExportProviderFactory;
        }
    }
}