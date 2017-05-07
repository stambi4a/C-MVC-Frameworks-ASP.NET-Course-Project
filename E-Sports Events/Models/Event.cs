namespace Models
{
    using System;
    using System.Collections.Generic;

    using Helpers.Enums;

    using Models.Images;

    public class Event
    {
        //private ICollection<Sponsor> sponsors;
        private ICollection<Player> players;
        private ICollection<Map> mapPool;
        private ICollection<Match> matches;
        private ICollection<RegisteredUser> users;

        public Event()
        {
            //this.sponsors = new HashSet<Sponsor>();
            this.players = new HashSet<Player>();
            this.users = new HashSet<RegisteredUser>();
            this.mapPool = new HashSet<Map>();
            this.matches = new HashSet<Match>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

       // public ICollection<Sponsor> Sponsors => this.sponsors;

        public double PrizePool { get; set; }

        public TierType TierType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Map> MapPool => this.mapPool;

        public virtual ICollection<Player> Players => this.players;

        public ICollection<Match> Matches => this.matches;

        public virtual ICollection<RegisteredUser> Users => this.users;

        public int LogoId { get; set; }

        public virtual Logo Logo { get; set; }
    }
}
