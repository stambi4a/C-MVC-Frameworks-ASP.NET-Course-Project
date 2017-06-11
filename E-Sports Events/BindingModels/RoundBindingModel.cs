namespace BindingModels
{
    using Helpers.Enums;

    public class RoundBindingModel
    {
        public int Id { get; set; }

        public RoundType RoundType { get; set; }

        public RoundFormat RoundFormat { get; set; }
    }
}
