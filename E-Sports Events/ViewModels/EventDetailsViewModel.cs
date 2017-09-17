namespace ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Helpers;
    using Helpers.Enums;

    public class EventDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double PrizePool { get; set; }

        public TierType TierType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public LogoViewModel Logo { get; set; }

        public SeasonViewModel Season { get; set; }

        public string PrizePoolToString => this.PrizePool + " лв.";

        public string SeasonToString => $"{this.Season.Year}";

        public string Description { get; set; }

        [Display(Name = "Event Admins")]
        public IEnumerable<EventAdminViewModel> EventAdmins { get; set; }

        public IEnumerable<PlayerViewModel> Players { get; set; }

        public string EventAdminsToString => string.Join(", ", this.EventAdmins);

        public string PlayersToString => string.Join(", ", this.Players);

        public CountryShortViewModel Country { get; set; }

        public CityShortViewModel City { get; set; }

        public string Location => ViewModelsMethods.GetLocation(this.Country.Name, this.City.Name);

        public VenueViewModel Venue { get; set; }

        public override string ToString()
        {
            return string.Join(", ", this.EventAdmins.ToString());
        }
    }
}
