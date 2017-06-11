namespace BindingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationAttributes;

    using Helpers.Enums;

    public class EventBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Range(100,100000, ErrorMessage = "The {0} must be between {1} and {2} лв.")]
        [Display(Name = "Prize Pool")]
        public double PrizePool { get; set; }

        public string PrizePoolToString => this.PrizePool + " лв";

        [Display(Name = "Tier Type")]
        public TierType TierType { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy HH:MM")]
        [StartDate(ErrorMessage = "Start Date should be later than today.")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy HH:MM")]
        [EndDate("StartDate", ErrorMessage = "End Date should be later than today.")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [StringLength(250, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public CountryBindingModel Country { get; set; }

        public int CountryId { get; set; }

        public IEnumerable<CountryBindingModel> AvailableCountries { get; set; }

        public CityBindingModel City { get; set; }

        public int CityId { get; set; }

        public IEnumerable<CityBindingModel> AvailableCities { get; set; }

        public VenueBindingModel Venue { get; set; }

        public int VenueId { get; set; }

        public IEnumerable<VenueBindingModel> AvailableVenues { get; set; }
    }
}
