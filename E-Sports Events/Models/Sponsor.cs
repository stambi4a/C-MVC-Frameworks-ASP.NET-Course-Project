namespace Models
{
    using System.Collections.Generic;

    public class Sponsor
    {
        private ICollection<Event> events;

        public Sponsor()
        {
            this.events = new HashSet<Event>();
        }

        public int Id { get; set; }

        public virtual ICollection<Event> Events => this.events;

        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}