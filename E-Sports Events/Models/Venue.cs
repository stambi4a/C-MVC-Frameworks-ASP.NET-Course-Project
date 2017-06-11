namespace Models
{
    using System.Collections.Generic;

    public class Venue
    {
        private ICollection<Event> events;
        private ICollection<Round> rounds;

        public Venue()
        {
            this.events = new HashSet<Event>();
            this.rounds = new HashSet<Round>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events => this.events;

        public ICollection<Round> Rounds => this.rounds;

        public virtual Country Country { get; set; }

        public virtual City City { get; set; }
    }
}
