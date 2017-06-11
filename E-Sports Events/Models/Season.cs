namespace Models
{
    using System.Collections.Generic;

    public class Season
    {
        private ICollection<Event> events;

        public Season()
        {
            this.events = new HashSet<Event>();
        }

        public int Id { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Event> Events => this.events;
    }
}
