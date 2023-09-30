namespace WalletApi.Model.DTOs
{
    public class CardInfoDTO
    {
        public int CardId { get; set; }
        public decimal Balance { get; set; }
        public decimal Available { get; set; }
        public string DailyPoints { get; set; }
        public string Month { get; set; }
    }
}
