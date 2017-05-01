namespace Models
{
    using System;
    using System.Collections.Generic;

    using Models.Enums;

    public class Event
    {
        private ICollection<Sponsor> sponsors;
        private ICollection<ApplicationUser> players;
        private ICollection<Map> mapPool;
        private ICollection<Match> matches;

        public Event()
        {
            this.sponsors = new HashSet<Sponsor>();
            this.players = new HashSet<ApplicationUser>();
            this.mapPool = new HashSet<Map>();
            this.matches = new HashSet<Match>();
        }
        public string Name { get; set; }

        public string Location { get; set; }

        public ICollection<Sponsor> Sponsors => this.sponsors;

        public TournamentType TournamentType { get; set; }

        public double PrizePool { get; set; }

        public TierType TierType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Map> MapPool => this.mapPool;

        public virtual ICollection<ApplicationUser> Players => this.players;

        public ICollection<Match> Matches => this.matches;

        public string EventAdminId { get; set; }

        public virtual ApplicationUser EventAdministrator { get; set; }
    }
}
