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
            CreateMap<CreateTransaction, Transaction>()
                .ForMember(m => m.AuthorizedUserId, cfg => cfg.MapFrom(e => e.UserId));
            CreateMap<Transaction, TransactionInfo>()
                .ForMember(m => m.User, cfg => cfg.MapFrom(e => e.AuthorizedUser.Name));
            CreateMap<Transaction, TransactionDetails>()
                .IncludeBase<Transaction, TransactionInfo>();
        }
    }
}
