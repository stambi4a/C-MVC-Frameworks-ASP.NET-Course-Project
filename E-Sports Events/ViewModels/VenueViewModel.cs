namespace ViewModels
{
    using System.Collections.Generic;

    using Helpers;

    public class VenueViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }

        public IEnumerable<RoundViewModel> Rounds { get; set; }

        public string EventsToString => string.Join(", ", this.Events);

        public string RoundsToString => string.Join(", ", this.Rounds);

        public CountryShortViewModel Country { get; set; }

        public CityShortViewModel City { get; set; }

        public string Location => ViewModelsMethods.GetLocation(this.Country.Name, this.City.Name);

        public override string ToString()
        {
            return this.Name;
        }
    }
}
