using Model.Contract;
using System.Collections.Generic;

namespace Model.Core.List {
    public class ReadOnlyInterfaceList<TModel> : List<TModel> where TModel : class, IModel {
    }
}