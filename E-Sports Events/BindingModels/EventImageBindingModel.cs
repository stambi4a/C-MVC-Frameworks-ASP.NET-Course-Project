namespace BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class EventImageBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Caption { get; set; }
    }
}
