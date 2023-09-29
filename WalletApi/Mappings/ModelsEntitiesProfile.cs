using AutoMapper;
using WalletApi.DataAccess.Entities;
using WalletApi.Model.Services;

namespace WalletApi.Mappings
{
    public class ModelsEntitiesProfile : AutoMapper.Profile
    {
        public ModelsEntitiesProfile()
        {
            MapTransactions();
        }

        public void MapTransactions() 
        {
            CreateMap<CreateTransaction, Transaction>();
            CreateMap<Transaction, TransactionInfo>()
                .ForMember(m => m.User, cfg => cfg.MapFrom(e => e.Card.User.Name));
            CreateMap<Transaction, TransactionDetails>()
                .IncludeBase<Transaction, TransactionInfo>();
        }
    }
}
