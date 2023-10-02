using System.ComponentModel.DataAnnotations;

namespace WalletApi.Model.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public required string Name { get; set; }
    }
}
