namespace Models
{
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class Sponsor: IdentityUser
    {
        private ICollection<Event> events;

        public Sponsor()
        {
            this.events = new HashSet<Event>();
        }

        public string CompanyName { get; set; }


        public virtual ICollection<Event> Events => this.events;
    }
}
