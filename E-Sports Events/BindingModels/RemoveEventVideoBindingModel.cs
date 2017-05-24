namespace BindingModels
{
    using System.Collections.Generic;
    public class RemoveEventVideoBindingModel
    {
        public string Name { get; set; }

        public IEnumerable<RemEventVideoBindingModel> AddedEventVideos { get; set; }

        public RemEventVideoBindingModel VideoToRemove { get; set; }
    }
}
