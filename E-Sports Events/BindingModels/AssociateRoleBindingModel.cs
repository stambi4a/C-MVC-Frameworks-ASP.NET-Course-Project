namespace BindingModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class AssociateRoleBindingModel
    {
        public string Id { get; set; }

        private List<string> allRoles = new List<string>
                                                   {
                                                       "Admin",
                                                       "ArticleAuthor",
                                                       "EventAdmin",
                                                       "Volunteer",
                                                       "Guest",
                                                       "Buyer",
                                                       "Seller"
                                                   };

        public string Username { get; set; }

        public IEnumerable<string> AssociatedRoles { get; set; }

        public string RolesToString => string.Join(", ", this.AssociatedRoles);

        public IEnumerable<string> AvailableRoles => this.allRoles.Except(this.AssociatedRoles);

        public string AddedRole { get; set; }

        public override string ToString()
        {
            return string.Join(",", this.AssociatedRoles);
        }
    }
}
