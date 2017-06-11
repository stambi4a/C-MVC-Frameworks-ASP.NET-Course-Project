namespace Models.Images
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Flags")]
    public class Flag : Image
    {
        [Key, ForeignKey("Country")]
        public new int Id { get; set; }

        public virtual Country Country{ get; set; }
    }
}
