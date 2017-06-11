using System;
namespace Models
{
    using System.Collections.Generic;

    public class City
    {
        private ICollection<Player> players;
        private ICollection<Team> teams;

        public City()
        {
            this.players = new HashSet<Player>();
            this.teams = new HashSet<Team>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Player> Players => this.players;

        public virtual ICollection<Team> Teams => this.teams;
    }
}
