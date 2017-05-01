namespace Models
{
    using System.Collections.Generic;

    using Models.Images;

    public class Player
    {
        private ICollection<Match> matches;

        public Player()
        {
            this.matches = new HashSet<Match>();
        }

        public int Id { get; set; }

        public double PrizeMoney { get; set; }

        public int BespaPoints { get; set; }

        public virtual PlayerImage PlayerImage { get; set; }

        public virtual ICollection<Match> Matches => this.matches;
    }
}
