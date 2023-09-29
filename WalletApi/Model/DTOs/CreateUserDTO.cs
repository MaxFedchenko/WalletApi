using System.ComponentModel.DataAnnotations;

namespace WalletApi.Model.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
