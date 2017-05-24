namespace BindingModels
{
    using System.Collections.Generic;
    public class RemoveEventImageBindingModel
    {
        public string Name { get; set; }

        public IEnumerable<RemEventImageBindingModel> AddedEventImages { get; set; }

        public RemEventImageBindingModel ImageToRemove { get; set; }
    }
}
