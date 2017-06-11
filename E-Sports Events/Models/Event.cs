namespace Models
{
    using System;
    using System.Collections.Generic;

    using Helpers.Enums;

    using Models.Images;
    using Models.Videos;

    public class Event
    {
        private ICollection<Player> players;
        private ICollection<Map> mapPool;
        private ICollection<Match> matches;
        private ICollection<RegisteredUser> users;
        private ICollection<RegisteredUser> eventAdmins;
        private ICollection<EventImage> eventImages;
        private ICollection<EventVideo> eventVideos;
        private ICollection<PlayerEventRanking> playerEventRankings;

        public Event()
        {
            this.players = new HashSet<Player>();
            this.users = new HashSet<RegisteredUser>();
            this.mapPool = new HashSet<Map>();
            this.matches = new HashSet<Match>();
            this.eventImages = new HashSet<EventImage>();
            this.eventAdmins = new HashSet<RegisteredUser>();
            this.eventVideos = new HashSet<EventVideo>();
            this.playerEventRankings = new SortedSet<PlayerEventRanking>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        //public string Location { get; set; }

        public double? PrizePool { get; set; }

        public TierType TierType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual ICollection<Map> MapPool => this.mapPool;

        public virtual ICollection<Player> Players => this.players;

        public ICollection<Match> Matches => this.matches;

        public virtual ICollection<RegisteredUser> Users => this.users;

        public virtual ICollection<RegisteredUser> EventAdmins => this.eventAdmins;

        public virtual ICollection<EventImage> EventImages => this.eventImages;

        public virtual ICollection<EventVideo> EventVideos => this.eventVideos;

        public virtual ICollection<PlayerEventRanking> PlayerEventRankings => this.playerEventRankings;

        public virtual Season Season { get; set; }

        public virtual Logo Logo { get; set; }

        public string Description { get; set; }

        public virtual City City { get; set; }

        public virtual Country Country { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
