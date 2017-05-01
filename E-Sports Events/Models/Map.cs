namespace Models
{
    using System.Collections.Generic;

    using Models.Enums;
    using Models.Images;

    public class Map
    {
        private ICollection<Game> games;
        private ICollection<Event> events;

        public Map()
        {
            this.games = new HashSet<Game>();
            this.events = new HashSet<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public MapSize MapSize { get; set; }

        public int MapImageId { get; set; }

        public virtual MapImage MapImage { get; set; }

        public ICollection<Game> Games { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
