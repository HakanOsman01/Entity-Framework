using System.ComponentModel.DataAnnotations;

namespace Rental.Infrustructer.DataBase.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = null!;
        [Required]
        public decimal Area { get; set; }
        public decimal? Price { get; set; }




    }
}
