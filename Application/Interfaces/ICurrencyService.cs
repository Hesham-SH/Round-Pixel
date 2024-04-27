using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface ICurrencyService : IBaseService
{
    Task<int> Add(CurrencyDto currencyDto);
    Task<Currency> GetByCodeAsync(string code);
    Task<int> UpdateAsync(CurrencyDto currencyDto);
}