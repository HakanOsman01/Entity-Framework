using MusicHub.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models
{
    public class Song
    {
        public Song()
        {
            this.SongsPerformers = new HashSet<SongPerformer>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]  
        public Genre Genre { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int? AlbumId { get; set; }
        [ForeignKey(nameof(AlbumId))]
        public Album? Album { get; set; }
        public int WriterId { get; set; }
        [ForeignKey(nameof(WriterId))]
        public virtual Writer Writer { get; set; }
        public virtual ICollection<SongPerformer> SongsPerformers { get; set; }


    }
}
