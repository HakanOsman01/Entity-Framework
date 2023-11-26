using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MusicHub.Data.Models
{
    public class Producer
    {
        public Producer()
        {
            this.Albums=new HashSet<Album>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public string? Pseudonym { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Album>Albums { get; set; }



    }
}