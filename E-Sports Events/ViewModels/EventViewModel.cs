namespace ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Helpers;
    using Helpers.Enums;

    public class EventViewModel
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

        public string EventAdminsToString => string.Join(", ", this.EventAdmins);

        public CountryShortViewModel Country { get; set; }

        public CityShortViewModel City { get; set; }

        public string Location => ViewModelsMethods.GetLocation(this.Country.Name, this.City.Name);

        public VenueShortViewModel Venue { get; set; }
    }
}
