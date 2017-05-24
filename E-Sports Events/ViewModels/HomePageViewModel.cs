namespace ViewModels
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public IEnumerable<EventImageViewModel> LatestEventImages { get; set; }

        public IEnumerable<EventVideoViewModel> LatestEventVideos { get; set; }
    }
}
