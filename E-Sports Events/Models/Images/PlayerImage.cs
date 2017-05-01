namespace Models.Images
{
    public class PlayerImage : Image
    {
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
