namespace ViewModels
{
    using System;
    using System.Collections.Generic;

    using Helpers;
    using Helpers.Enums;

    public class EventAllDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double PrizePool { get; set; }

        public TierType TierType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public LogoViewModel Logo { get; set; }

        public string PrizePoolToString => this.PrizePool + " лв.";

        public string Description { get; set; }

        public IEnumerable<PlayerViewModel> Players { get; set; }

        public string PlayersToString => string.Join(", ", this.Players);

        public CountryShortViewModel Country { get; set; }

        public CityShortViewModel City { get; set; }

        public VenueViewModel Venue { get; set; }

        public IEnumerable<EventImageViewModel> EventImages { get; set; }

        public IEnumerable<EventVideoViewModel> EventVideos { get; set; }

        public string Location => ViewModelsMethods.GetLocation(this.Country.Name, this.City.Name);
    }
}
