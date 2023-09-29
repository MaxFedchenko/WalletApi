using System.ComponentModel.DataAnnotations;
using WalletApi.Core.Enums;

namespace WalletApi.Model.DTOs
{
    public class CreateTransactionDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public bool IsPending { get; set; }
        [Required]
        public int UserId { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
    }
}
