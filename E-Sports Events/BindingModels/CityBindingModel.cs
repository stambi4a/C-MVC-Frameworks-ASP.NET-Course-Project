namespace BindingModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CityBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Available Countries")]
        public IEnumerable<CountryBindingModel> AvailableCountries { get; set; }

        public CountryBindingModel Country { get; set; }

        public int CountryId { get; set; }

    }
}
