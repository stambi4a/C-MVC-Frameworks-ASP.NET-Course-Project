namespace Models.Images
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TeamLogos")]
    public class TeamLogo : Image
    {
        [Key, ForeignKey("Team")]
        public new int Id { get; set; }

        public virtual Team Team{ get; set; }
    }
}
