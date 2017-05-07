namespace BindingModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class DissociateRoleBindingModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> AssociatedRoles { get; set; }

        public string RolesToString => string.Join(", ", this.AssociatedRoles);

        //public IEnumerable<string> AvailableRoles => this.allRoles.Except(this.AssignedRoles);

        public string RoleToRemove { get; set; }

        public override string ToString()
        {
            return string.Join(",", this.AssociatedRoles);
        }
    }
}
