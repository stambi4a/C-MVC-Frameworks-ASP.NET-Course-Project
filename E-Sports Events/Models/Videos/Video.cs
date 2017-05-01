namespace Models.Videos
{
    public abstract class Video
    {
        public int Id { get; set; }

        public string Caption { get; set; }

        public string Url { get; set; }
    }
}
