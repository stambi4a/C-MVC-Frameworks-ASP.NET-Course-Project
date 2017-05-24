namespace Models.Videos
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EventVideos")]
    public class EventVideo: Video
    {
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
