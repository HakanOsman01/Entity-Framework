using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models
{
    public class Performer
    {
        public Performer()
        {
            this.SongsPerformers = new HashSet<SongPerformer>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; } = null!;
        [Required]
        public int Age { get; set; }
        [Required]
        public decimal NetWorth { get; set; }
        public virtual ICollection<SongPerformer> SongsPerformers { get; set; }
    }
}
