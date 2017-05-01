namespace Models.Images
{
    using System.Collections.Generic;

    public class ArticleImage :Image
    {
        private ICollection<Article> articles;

        public ArticleImage()
        {
            this.articles = new HashSet<Article>();
        }

        public virtual ICollection<Article> Articles => this.articles;
    }
}
