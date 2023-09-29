namespace WalletApi.DataAccess.Entities
{
    public abstract class Entity<K> where K : struct
    {
         public K Id { get; set; }
    }
}
