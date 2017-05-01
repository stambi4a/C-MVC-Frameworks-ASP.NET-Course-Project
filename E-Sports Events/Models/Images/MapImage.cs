namespace Models.Images
{
    public class MapImage : Image
    {
        public int MapId { get; set; }
        public virtual Map Map { get; set; }
    }
}
