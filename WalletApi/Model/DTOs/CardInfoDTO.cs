namespace WalletApi.Model.DTOs
{
    public class CardInfoDTO
    {
        public required int CardId { get; set; }
        public required decimal Balance { get; set; }
        public required decimal Available { get; set; }
        public required string DailyPoints { get; set; }
        public required string Month { get; set; }
    }
}
