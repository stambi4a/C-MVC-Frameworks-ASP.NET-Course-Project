namespace BindingModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VenueBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Available Countries")]
        public IEnumerable<CountryBindingModel> AvailableCountries { get; set; }

        public int CountryId { get; set; }

        [Display(Name = "Available Cities")]
        public IEnumerable<CityBindingModel> AvailableCities { get; set; }

        public int CityId { get; set; }

        public CountryBindingModel Country { get; set; }

        public CityBindingModel City { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
