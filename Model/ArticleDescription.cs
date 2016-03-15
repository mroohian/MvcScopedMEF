using Model.Contract;

namespace Model {
    public class ArticleDescription : IDescriptionModel {
        public string Title { get; set; }

        public int LanguageId { get; set; }
    }
}
