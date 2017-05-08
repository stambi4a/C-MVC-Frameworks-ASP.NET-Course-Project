namespace Models.Images
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PlayerImages")]
    public class PlayerImage : Image
    {
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
