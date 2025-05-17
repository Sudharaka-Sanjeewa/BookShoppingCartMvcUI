using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUI.Models.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Email { get; set; }

        public int Mobile { get; set; }
    }
}
