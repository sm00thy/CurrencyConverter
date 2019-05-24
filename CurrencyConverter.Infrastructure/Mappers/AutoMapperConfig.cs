using AutoMapper;
using CurrencyConverter.Core.Entities;
using CurrencyConverter.Infrastructure.Dto;

namespace CurrencyConverter.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CurrencyConvert, CurrencyConverterDto>();
                })
                .CreateMapper();
        
    }
}