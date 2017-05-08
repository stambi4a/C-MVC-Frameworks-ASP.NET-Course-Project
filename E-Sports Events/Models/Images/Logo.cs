namespace Models.Images
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Logos")]
    public class Logo : Image
    {
        [Key, ForeignKey("Event")]
        public new int Id { get; set; }

        public virtual Event Event{ get; set; }
    }
}
