namespace BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BindingModels.ValidationAttributes;

    using Helpers.Enums;

    public class EventDetailsBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Range(100, 10000, ErrorMessage = "The {0} must be between {1} and {2}")]
        [Display(Name = "Prize Pool")]
        public double PrizePool { get; set; }

        //[Required]
        [Display(Name = "Tier Type")]
        public TierType TierType { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [StartDate(ErrorMessage = "Start Date should be later than today.")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [EndDate("StartDate", ErrorMessage = "End Date should be later than today.")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Url]
        [Display(Name = "End Date")]
        public LogoBindingModel Logo { get; set; }
    }
}
