namespace BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class EventVideoBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public string Caption { get; set; }
    }
}
