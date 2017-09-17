namespace ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Helpers;

    public class TeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryShortViewModel Country { get; set; }

        public CityShortViewModel City { get; set; }

        public string Location => ViewModelsMethods.GetLocation(this.Country.Name, this.City.Name);

        public IEnumerable<PlayerViewModel> Players { get; set; }

        public string PlayersToString => this.Players.Any() ? string.Join(", ", this.Players) : string.Empty;

        public TeamLogoViewModel TeamLogo { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
