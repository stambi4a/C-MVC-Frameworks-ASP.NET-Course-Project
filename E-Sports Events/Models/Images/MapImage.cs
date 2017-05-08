namespace Models.Images
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MapImages")]
    public class MapImage : Image
    {
        public int MapId { get; set; }
        public virtual Map Map { get; set; }
    }
}
