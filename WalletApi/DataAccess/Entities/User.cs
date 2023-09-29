using System.ComponentModel.DataAnnotations;

namespace WalletApi.DataAccess.Entities
{
    public class User : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        public Card Card { get; set; }
    }
}
