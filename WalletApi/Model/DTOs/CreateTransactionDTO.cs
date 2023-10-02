using System.ComponentModel.DataAnnotations;

namespace WalletApi.Model.DTOs
{
    public class CreateTransactionDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage="Sum can't be negative")]
        public required decimal Sum { get; set; }
        [Required]
        public required bool IsPending { get; set; }
        [Required]
        public required int CardId { get; set; }
        [Required]
        public required int UserId { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
    }
}
