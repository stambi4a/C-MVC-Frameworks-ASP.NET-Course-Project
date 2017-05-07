namespace Models.Images
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Logo")]
    public class Logo : Image
    {
        public int EventId { get; set; }

        public virtual Event Event{ get; set; }
    }
}
