using Microsoft.AspNetCore.WebUtilities;
using WalletApi.Core.Enums;
using WalletApi.Model.DTOs;
using WalletApi.Model.Services;

namespace WalletApi.Mappings
{
    public class ModelsDTOsProfile : AutoMapper.Profile
    {
        public ModelsDTOsProfile() 
        {
            MapTransactions();
        }

        private string ConvTranDate(DateTime date)
        {
            var days_ago = (DateTime.Now.Date - date.Date).TotalDays;

            if (days_ago > 6)
                return date.ToString("dd-MM-yy");
            else if (days_ago == 0)
                return "Today";
            else if (days_ago == 1)
                return "Yesterday";
            else
                return date.DayOfWeek.ToString();
        }

        public void MapTransactions()
        {
            CreateMap<CreateTransactionDTO, CreateTransaction>()
                .ForMember(m => m.Type, cfg => cfg.MapFrom(d => Enum.Parse<TransactionType>(d.Type, true)))
                .ForMember(m => m.Icon, cfg => cfg.MapFrom(d => string.IsNullOrWhiteSpace(d.Icon) ? 
                    null : WebEncoders.Base64UrlDecode(d.Icon.Split(',', StringSplitOptions.None)[1])));

            CreateMap<TransactionInfo, TransactionDTO>()
                .ForMember(d => d.Type, cfg => cfg.MapFrom(m => m.Type.ToString()))
                .ForMember(d => d.Date, cfg => cfg.MapFrom(m => ConvTranDate(m.Date)))
                .ForMember(d => d.Icon, cfg => cfg.MapFrom(m => m.Icon != null && m.Icon.Length > 0 ? 
                    "data:image/svg;base64," + WebEncoders.Base64UrlEncode(m.Icon).Replace('-', '+').Replace('_', '/') : null));

            CreateMap<TransactionDetails, TransactionDetailsDTO>()
                .IncludeBase<TransactionInfo, TransactionDTO>();
        }
    }
}
