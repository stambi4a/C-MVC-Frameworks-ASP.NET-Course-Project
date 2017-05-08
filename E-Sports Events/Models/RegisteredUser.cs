namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RegisteredUser : IdentityUser
    {
        private ICollection<Event> events;
        private ICollection<Event> administratedEvents;
        private ICollection<Article> articles;
        public RegisteredUser()
        {
            this.events = new HashSet<Event>();
            this.articles = new HashSet<Article>();
            this.administratedEvents = new HashSet<Event>();
        }

        public string Name { get; set; }

        public virtual ICollection<Event> Events => this.events;
        public virtual ICollection<Event> AdministratedEvents => this.administratedEvents;

        public ICollection<Article> Articles => this.articles;

        public DateTime? DateAdded { get; set; }

        //public string UserId { get; set; }
        //public virtual IdentityUser User{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<RegisteredUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
