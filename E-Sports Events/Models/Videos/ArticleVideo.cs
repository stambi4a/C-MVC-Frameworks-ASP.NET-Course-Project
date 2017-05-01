namespace Models.Videos
{
    using System.Collections.Generic;

    public class ArticleVideo: Video
    {
        private ICollection<Article> articles;

        public ArticleVideo()
        {
            this.articles = new HashSet<Article>();
        }

        public virtual ICollection<Article> Type => this.articles;
    }
}
