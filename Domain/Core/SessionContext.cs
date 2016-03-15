using System.Collections.Generic;
using System.Composition;
using DepInj.Contract;
using Domain.Contract.Core;

namespace Domain.Core {
    // ReSharper disable once ClassNeverInstantiated.Global
    [Export(typeof(ISessionContext))]
    [Shared(MefBoundaries.Session)]
    public class SessionContext : ISessionContext {
        private readonly ICore _core;
        public int LanguageId { get; set; }

        [ImportingConstructor]
        public SessionContext(ICore core) {
            _core = core;
        }

        void IDebugInfoProvider.AddDebugLines(string prefix, Dictionary<string, string> debugLines) {
            _core.AddDebugLines($"{prefix} - Core", debugLines);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString() {
            return $"[Language={LanguageId}, HashCode={GetHashCode()}]";
        }
    }
}