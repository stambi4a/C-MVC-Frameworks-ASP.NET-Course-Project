namespace ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class CountryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<PlayerViewModel> Players { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }

        public IEnumerable<TeamViewModel> Teams { get; set; }

        public string PlayersToString => this.Players.Any() ? string.Join(", ", this.Players) : "No Players";

        public string CitiesToString => this.Cities.Any() ? string.Join(", ", this.Cities) : "No Cities";

        public string TeamsToString => this.Teams.Any() ? string.Join(", ", this.Teams) : "No Teams";

        public FlagViewModel Flag { get; set; }
    }
}
