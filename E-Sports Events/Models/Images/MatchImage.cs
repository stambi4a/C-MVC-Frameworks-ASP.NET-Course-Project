namespace Models.Images
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MatchImages")]
    public class MatchImage: Image
    {
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}
