namespace Models
{
    using System.Collections.Generic;

    using Helpers.Enums;

    public class Round
    {
        private ICollection<Match> matches;

        public Round()
        {
            this.matches = new HashSet<Match>();
        }

        public int Id { get; set; }

        public RoundType RoundType { get; set; }

        public RoundFormat RoundFormat { get; set; }

        public virtual ICollection<Match> Matches => this.matches;

        public virtual Venue Venue { get; set; }
    }
}
