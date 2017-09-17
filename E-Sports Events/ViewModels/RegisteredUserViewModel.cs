namespace ViewModels
{
    using System;
    using System.Collections.Generic;

    public class RegisteredUserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string  Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
