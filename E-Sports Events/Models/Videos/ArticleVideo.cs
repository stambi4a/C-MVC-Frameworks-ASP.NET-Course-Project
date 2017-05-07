namespace Models.Videos
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ArticleVideo")]
    public class ArticleVideo: Video
    {
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
