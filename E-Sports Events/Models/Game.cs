namespace Models
{
    using Models.Enums;

    public class Game
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match{ get; set; }

        public int MapId { get; set; }

        public Map Map { get; set; }

        public int FirstPlayerId { get; set; }

        public virtual ApplicationUser FirstPlayer { get; set; }

        public int SecondPlayerId { get; set; }

        public virtual ApplicationUser SecondPlayer { get; set; }

        public Winner Winner { get; set; }
    }
}
