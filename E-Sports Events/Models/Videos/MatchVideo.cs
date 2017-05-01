namespace Models.Videos
{
    using System.Collections.Generic;

    public class MatchVideo : Video
    {
        private ICollection<Match> matches;

        public MatchVideo()
        {
            this.matches = new HashSet<Match>();
        }

        public virtual ICollection<Match> Matches => this.matches;
    }
}
