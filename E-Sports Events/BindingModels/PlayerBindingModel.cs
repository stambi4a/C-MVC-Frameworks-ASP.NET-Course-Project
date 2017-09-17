namespace BindingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationAttributes;

    public class PlayerBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Range(0, 1000000, ErrorMessage = "The {0} must be between {1} and {2} лв.")]
        [Display(Name = "Prize Money")]
        public double PrizeMoney { get; set; }

        [Required]
        [Range(0, 50000, ErrorMessage = "The {0} must be between {1} and {2} points")]
        [Display(Name = "Bespa Points")]
        public int BespaPoints { get; set; }

        //[Required]
        public TeamBindingModel Team { get; set; }

        //[Required]
        public CountryBindingModel Country { get; set; }

        public CityBindingModel City { get; set; }

        public IEnumerable<CountryBindingModel> AvailableCountries { get; set; }

        public IEnumerable<CityBindingModel> AvailableCities { get; set; }

        public IEnumerable<TeamBindingModel> AvailableTeams { get; set; }

        //public string DateOfBirthToString => this.DateOfBirth.ToString("d");

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [DateOfBirth(ErrorMessage = "Player should be between 14 and 70 years old.")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public int TeamId { get; set; }

        public PlayerImageBindingModel PlayerImage { get; set; }
    }
}
