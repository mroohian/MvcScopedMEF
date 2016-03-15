using System;
using System.Linq;
using Model.Contract;
using Model.List;

namespace Model {
    public class Article : IModel, IHaveDescription<ArticleDescription> {

        public Article() {
            ArticleId = Int32.MinValue;
            DescriptionList = new ArticleDescriptionList();
        }

        public int ArticleId { get; set; }


        public ArticleDescriptionList DescriptionList { get; set; }
    }

    public class ArticleStateLess : Article {
        public ArticleDescription Description(int languageId) {
            return DescriptionList.FirstOrDefault(article => article.LanguageId == languageId);
        }
    }
}
