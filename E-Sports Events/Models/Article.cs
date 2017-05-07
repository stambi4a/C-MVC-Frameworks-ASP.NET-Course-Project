namespace Models
{
    using System;
    using System.Collections.Generic;

    using Models.Images;
    using Models.Videos;

    public class Article
    {
        private ICollection<ArticleImage> images;
        private ICollection<ArticleVideo> videos;

        public Article()
        {
            this.images = new HashSet<ArticleImage>();
            this.videos = new HashSet<ArticleVideo>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual RegisteredUser Author { get; set; }

        public virtual ICollection<ArticleImage> Images => this.images;

        public ICollection<ArticleVideo> Videos => this.videos;
    }
}