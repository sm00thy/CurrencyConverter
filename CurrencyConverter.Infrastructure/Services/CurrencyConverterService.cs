using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CurrencyConverter.Core.Entities;
using CurrencyConverter.Infrastructure.Commands;
using CurrencyConverter.Infrastructure.Dto;
using CurrencyConverter.Infrastructure.Repositories.Interfaces;
using CurrencyConverter.Infrastructure.Services.Interfaces;
using RestSharp;

namespace CurrencyConverter.Infrastructure.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly IRepository<CurrencyTable> _repository;
        private readonly IRepository<CurrencyConvert> _currencyRepository;
        private readonly IMapper _mapper;
        private readonly RestClient _client = new RestClient("http://api.nbp.pl/api/exchangerates");

        public CurrencyConverterService(IRepository<CurrencyTable> repository,
            IRepository<CurrencyConvert> currencyRepository,
            IMapper mapper)
        {
            _repository = repository;
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurrenciesListDto>> GetCurrencyListAsync()
        {
            var list = new List<RateFromTable>()
            {
                new RateFromTable()
                {
                    Code = "eur",
                    Currency = "euro"
                },
                new RateFromTable()
                {
                    Code = "usd",
                    Currency = "us dollar"
                },
                new RateFromTable()
                {
                    Code = "gbp",
                    Currency = "great britain pound"
                },
                new RateFromTable()
                {
                    Code = "aud",
                    Currency = "australian dollar"
                },
                new RateFromTable()
                {
                    Code = "cad",
                    Currency = "canadian dollar"
                }
            };
            
            return _mapper.Map<IEnumerable<CurrenciesListDto>>(list);
        }

        public async Task<CurrencyConverterDto> ConvertCurrency(CurrencyToExchange toExchange)
        {
            if(toExchange.Amount < 0)
                throw new Exception("Amount to convert cannot be less than 0.");
            if(toExchange.ToCurrency == "pln")
                throw new Exception($"Currently convert to {toExchange.ToCurrency} is not supported.");
            
            var baseCurrencyRequest = new RestRequest($"rates/a/{toExchange.FromCurrency}",
                Method.GET, DataFormat.Json);
            var toCurrencyRequest = new RestRequest($"rates/a/{toExchange.ToCurrency}",
                Method.GET, DataFormat.Json);
            var baseCurrencyResponse = await _client
                .ExecuteTaskAsync<CurrencyTable>(baseCurrencyRequest);
            var toCurrencyResponse = await _client
                .ExecuteTaskAsync<CurrencyTable>(toCurrencyRequest);
            await _repository.AddAsync(baseCurrencyResponse.Data);
            await _repository.AddAsync(toCurrencyResponse.Data);

            var currentDayConvertToPrice = toCurrencyResponse.Data.Rates.Select(x => x.Mid).Single();
            if (toExchange.FromCurrency != "pln" && toExchange.ToCurrency != "pln")
            {
                var currentDayUnitPrice = baseCurrencyResponse.Data.Rates.Select(x => x.Mid).Single();
                var pricePerUnit = currentDayUnitPrice / currentDayConvertToPrice;
                var amountValue = currentDayUnitPrice * toExchange.Amount;
                var convertedValue = amountValue / currentDayConvertToPrice;

                var finalResponse = new CurrencyConvert()
                {
                    FromCurrency = toExchange.FromCurrency,
                    ToCurrency = toExchange.ToCurrency,
                    PricePerUnit = Math.Round(pricePerUnit, 2),
                    BaseCurrencyValue = toExchange.Amount,
                    ConvertedCurrencyValue = Math.Round(convertedValue, 2)
                };
                await _currencyRepository.AddAsync(finalResponse);
                return _mapper.Map<CurrencyConverterDto>(finalResponse);
            }
            
            var responseFromPlnExchange = new CurrencyConvert()
            {
                FromCurrency = toExchange.FromCurrency,
                ToCurrency = toExchange.ToCurrency,
                PricePerUnit = currentDayConvertToPrice,
                BaseCurrencyValue = toExchange.Amount,
                ConvertedCurrencyValue = toExchange.Amount * currentDayConvertToPrice
            };

            await _currencyRepository.AddAsync(responseFromPlnExchange);
            return _mapper.Map<CurrencyConverterDto>(responseFromPlnExchange);
        }
        
        
    }
}