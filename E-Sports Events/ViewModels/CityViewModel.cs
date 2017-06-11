namespace ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryShortViewModel Country { get; set; }

        public IEnumerable<PlayerViewModel> Players { get; set; }

        public string PlayersToString => this.Players.Any() ? string.Join(", ", this.Players) : "No Players";

        public IEnumerable<TeamViewModel> Teams { get; set; }

        public string TeamsToString => this.Teams.Any() ? string.Join(", ", this.Teams) : "No Teams";

        public override string ToString()
        {
            return this.Name;
        }
    }
}
