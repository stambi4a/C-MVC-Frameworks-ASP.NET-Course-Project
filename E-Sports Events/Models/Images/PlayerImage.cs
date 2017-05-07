namespace Models.Images
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PlayerImage")]
    public class PlayerImage : Image
    {
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
