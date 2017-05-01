namespace Models.Images
{
    public abstract class Image
    {
        public int Id { get; set; }

        public string Caption { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
