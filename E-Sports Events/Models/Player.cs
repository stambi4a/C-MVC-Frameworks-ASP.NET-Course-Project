namespace Models
{
    using System;
    using System.Collections.Generic;

    using Images;

    public class Player
    {
        private ICollection<Event> events;
        private ICollection<Match> matchesAsFirstPlayer;
        private ICollection<Match> matchesAsSecondPlayer;
        private ICollection<PlayerEventRanking> playerEventRankings;

        public Player()
        {
            this.matchesAsFirstPlayer = new HashSet<Match>();
            this.matchesAsSecondPlayer = new HashSet<Match>();
            this.events = new HashSet<Event>();
            this.playerEventRankings = new SortedSet<PlayerEventRanking>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public virtual Team Team { get; set; }

        public string Address { get; set; }

        public double PrizeMoney { get; set; }

        public int BespaPoints { get; set; }

        public virtual PlayerImage PlayerImage { get; set; }

        public virtual Country Country { get; set; }

        public virtual City City { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Event> Events => this.events;

        public virtual ICollection<Match> MatchesAsFirstPlayer => this.matchesAsFirstPlayer;

        public virtual ICollection<Match> MatchesAsSecondPlayer => this.matchesAsSecondPlayer;

        public virtual ICollection<PlayerEventRanking> PlayerEventRankings => this.playerEventRankings;
    }
}
