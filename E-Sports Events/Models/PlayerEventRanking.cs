namespace Models
{
    using System;
    public class PlayerEventRanking : IComparable<PlayerEventRanking>
    {
        public int Id { get; set; }

        public int Place { get; set; }

        public int BespaPoints { get; set; }

        public int PrizePool { get; set; }

        public virtual Event Event { get; set; }

        public virtual Player Player { get; set; }

        public int CompareTo(PlayerEventRanking other)
        {
            return this.Id - other.Id;
        }
    }
}
