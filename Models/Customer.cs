using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUI.Models
{
    [Table("Customer")]
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Email { get; set; }

        public int Mobile { get; set; }

    }
}
