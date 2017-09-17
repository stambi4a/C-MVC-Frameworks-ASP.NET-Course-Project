namespace ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RegisteredUserDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string  Name { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

        [Display(Name = "Date added")]
        public DateTime DateAdded { get; set; }

        public IEnumerable<BasicEventViewModel> Events { get; set; }

        public IEnumerable<BasicArticleViewModel> Articles { get; set; }
    }
}
