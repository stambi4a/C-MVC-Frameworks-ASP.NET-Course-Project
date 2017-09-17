namespace BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CountryBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public FlagBindingModel Flag { get; set; }
    }
}
