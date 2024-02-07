using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rental.Core.Models
{
    public class PropertyModel
    {
        public PropertyModel()
        {
            
        }

        [Required]
        [MaxLength(200)]
        [Display(Name="Адрес")]
        public string Location { get; set; } = null!;
        [Required]
        [Display(Name = "Площ")]
        public decimal Area { get; set; }
        [Display(Name ="Цена")]
        public decimal? Price { get; set; }
    }
}
