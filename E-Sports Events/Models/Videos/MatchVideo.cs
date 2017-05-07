namespace Models.Videos
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MatchVideo")]
    public class MatchVideo : Video
    {
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}
