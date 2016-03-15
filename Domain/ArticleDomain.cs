﻿using System.Collections.Generic;
using System.Composition;
using DepInj.Contract;
using Domain.Contract;
using Domain.Contract.Core;
using Domain.Core;

namespace Domain {
    [Export(typeof(IArticleDomain))]
    //[Shared(MefBoundries.Request)]
    [Shared(MefBoundaries.Session)]
    public class ArticleDomain : BaseDomain, IArticleDomain {
        [ImportingConstructor]
        public ArticleDomain(ICore core) : base(core) {}

        void IDebugInfoProvider.AddDebugLines(string prefix, Dictionary<string, string> debugLines) {
            Core.AddDebugLines($"{prefix} - Core", debugLines);

            debugLines[$"{prefix} - ISessionContext"] = SessionContext.GetHashCode().ToString();
            SessionContext.AddDebugLines($"{prefix} - ISessionContext", debugLines);
        }
    }
}
