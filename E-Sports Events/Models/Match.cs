namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Helpers.Enums;

    using Models.Images;
    using Models.Videos;

    public class Match
    {
        private ICollection<Game> games;
        private ICollection<MatchImage> images;
        private ICollection<MatchVideo> videos;

        public Match()
        {
            this.games = new HashSet<Game>();
            this.images = new HashSet<MatchImage>();
            this.videos = new HashSet<MatchVideo>();
        }

        public int Id { get; set; }

        public int MatchNumber { get; set; }

        public int FirstPlayerId { get; set; }

        public virtual Player FirstPlayer { get; set; }

        public int SecondPlayerId { get; set; }

        public virtual Player SecondPlayer { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<Game> Games => this.games;

        public bool IsFinished { get; set; }

        public Winner Winner => this.GetWinner();

        public string Result => this.GetResult();

        public virtual ICollection<MatchImage> Images => this.images;

        public ICollection<MatchVideo> Videos => this.videos;

        public virtual Round Round { get; set; }

        private string GetResult()
        {
            var countFirst = this.games.Count(g => g.Winner == Winner.First);
            var countSecond = this.games.Count(g => g.Winner == Winner.Second);

            return $"{countFirst}:{countSecond}";
        }

        private Winner GetWinner()
        {
            var countNonFinished = this.games.Count(g => g.Winner == Winner.None);
            if (countNonFinished > 0)
            {
                return Winner.None;
            }
            var countFirst = this.games.Count(g=>g.Winner == Winner.First);
            var countSecond = this.games.Count(g => g.Winner == Winner.Second);
            if (countFirst == countSecond)
            {
                throw new InvalidOperationException("Match should have odd number of games to gurantee a winner is fixed.");
            }

            return countFirst > countSecond ? Winner.First : Winner.Second;
        }
    }
}
