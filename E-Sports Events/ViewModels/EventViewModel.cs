﻿namespace ViewModels
{
    using System;

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

        public string LogoUrl { get; set; }
    }
}
