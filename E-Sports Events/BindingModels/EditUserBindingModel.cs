namespace BindingModels
{
    using System;

    public class EditUserBindingModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}
