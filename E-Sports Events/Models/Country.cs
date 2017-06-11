namespace Models
{
    using System.Collections.Generic;

    using Images;

    public class Country
    {
        private ICollection<Player> players;
        private ICollection<Team> teams;
        private ICollection<City> cities;

        public Country()
        {
            this.players = new HashSet<Player>();
            this.teams = new HashSet<Team>();
            this.cities = new HashSet<City>();
    }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Flag Flag { get; set; }

        public virtual ICollection<Player> Players => this.players;

        public virtual ICollection<Team> Teams => this.teams;

        public virtual ICollection<City> Cities => this.cities;
    }
}
