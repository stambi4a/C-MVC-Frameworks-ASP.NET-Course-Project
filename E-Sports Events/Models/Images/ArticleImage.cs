namespace Models.Images
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ArticleImages")]
    public class ArticleImage :Image
    {
        public int ArticleId { get; set; }

        public virtual Article Article{ get; set; }
    }
}
