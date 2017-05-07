namespace Models
{
    using System.Collections.Generic;

    using Models.Images;

    public class Player
    {
        //private ICollection<Match> matches;

        private ICollection<Event> events;
        private ICollection<Match> matchesAsFirstPlayer;
        private ICollection<Match> matchesAsSecondPlayer;

        public Player()
        {
            this.matchesAsFirstPlayer = new HashSet<Match>();
            this.matchesAsSecondPlayer = new HashSet<Match>();
            this.events = new HashSet<Event>();
        }

        public int Id { get; set; }

        public string Team { get; set; }

        public string Address { get; set; }

        public double PrizeMoney { get; set; }

        public int BespaPoints { get; set; }

        public virtual PlayerImage PlayerImage { get; set; }

        //public virtual ICollection<Match> Matches => this.matches;

        public virtual ICollection<Event> Events => this.events;

        public virtual ICollection<Match> MatchesAsFirstPlayer => this.matchesAsFirstPlayer;
        public virtual ICollection<Match> MatchesAsSecondPlayer => this.matchesAsSecondPlayer;
    }
}
