namespace WalletApi.Model.DTOs
{
    public class TransactionDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required decimal Sum { get; set; }
        public required string Date { get; set; }
        public required bool IsPending { get; set; }
        public required string User { get; set; }
        public string? Icon { get; set; }
    }
}
