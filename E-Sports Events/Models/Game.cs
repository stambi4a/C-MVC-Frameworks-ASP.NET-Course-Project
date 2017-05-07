namespace Models
{
    using Helpers.Enums;

    public class Game
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match{ get; set; }

        public int MapId { get; set; }

        public Map Map { get; set; }

        public int FirstPlayerId { get; set; }

        public virtual RegisteredUser FirstPlayer { get; set; }

        public int SecondPlayerId { get; set; }

        public virtual RegisteredUser SecondPlayer { get; set; }

        public Winner Winner { get; set; }
    }
}
