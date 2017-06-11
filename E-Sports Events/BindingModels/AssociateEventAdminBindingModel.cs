namespace BindingModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AssociateEventAdminBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Associated EventAdmins")]
        public IEnumerable<EventAdminBindingModel> AssociatedAdmins { get; set; }

        public string AssociatedAdminsToString => string.Join(", ", this.AssociatedAdmins);

        [Display(Name = "Available EventAdmins")]
        public IEnumerable<EventAdminBindingModel> AvailableAdmins { get; set; }

        public string AssociateEventAdminId { get; set; }
    }
}
