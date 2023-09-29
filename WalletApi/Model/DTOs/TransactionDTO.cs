namespace WalletApi.Model.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Sum { get; set; }
        public string Date { get; set; }
        public bool IsPending { get; set; }
        public string User { get; set; }
        public string Icon { get; set; }
    }
}
