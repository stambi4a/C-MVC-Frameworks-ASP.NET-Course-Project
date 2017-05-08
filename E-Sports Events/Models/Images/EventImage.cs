namespace Models.Images
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EventImages")]
    public class EventImage : Image
    {
        public int EventId { get; set; }
        public virtual Event Match { get; set; }
    }
}
