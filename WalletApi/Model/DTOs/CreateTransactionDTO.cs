using System.ComponentModel.DataAnnotations;

namespace WalletApi.Model.DTOs
{
    public class CreateTransactionDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage="Sum can't be negative")]
        public decimal Sum { get; set; }
        [Required]
        public bool IsPending { get; set; }
        [Required]
        public int CardId { get; set; }
        [Required]
        public int UserId { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
    }
}
