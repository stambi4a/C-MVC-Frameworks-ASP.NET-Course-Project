namespace BindingModels
{
    using System.Collections.Generic;

    public class AssociateEventAdminBindingModel
    {
        public string Name { get; set; }

        public IEnumerable<EventAdminBindingModel> AssociatedAdmins { get; set; }

        public string AssociatedAdminsToString => string.Join(", ", this.AssociatedAdmins);

        public IEnumerable<EventAdminBindingModel> AvailableAdmins { get; set; }

        public string AssociateEventAdminId { get; set; }
    }
}
