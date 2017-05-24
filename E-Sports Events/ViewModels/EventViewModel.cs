namespace ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Helpers.Enums;

    public class EventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public double PrizePool { get; set; }

        public TierType TierType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public LogoViewModel Logo { get; set; }

        [Display(Name = "Event Admins")]
        public IEnumerable<EventAdminViewModel> EventAdmins { get; set; }

        public string EventAdminsToString => string.Join(", ", this.EventAdmins);

        public string PrizePoolToString => this.PrizePool + " лв.";

        public string Description { get; set; }

        public override string ToString()
        {
            return string.Join(", ", this.EventAdmins.ToString());
        }
    }
}
