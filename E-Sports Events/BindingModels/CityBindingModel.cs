namespace BindingModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CityBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Available Countries")]
        public IEnumerable<CountryBindingModel> AvailableCountries { get; set; }

        public CountryBindingModel Country { get; set; }

        [Required]
        public int CountryId { get; set; }

    }
}
