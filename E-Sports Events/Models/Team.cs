namespace Models
{
    using System.Collections.Generic;

    using Models.Images;

    public class Team
    {
        private ICollection<Player> players;

        public Team()
        {
            this.players = new HashSet<Player>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Player> Players => this.players;

        public virtual TeamLogo TeamLogo { get; set; }
    }
}
