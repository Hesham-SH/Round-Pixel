using Application.Currencies;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;

namespace Application.Services;
public class CurrencyService : BaseService, ICurrencyService
{
    private readonly IMapper _mapper;
    public CurrencyService(IMapper mapper)
    {
        _mapper = mapper; 
    }
    public async Task<int> Add(CurrencyDto currencyDTO)
    {
        var currency = _mapper.Map<CurrencyDto, Currency>(currencyDTO);
        return await _mediator.Send(new Add.Command{ Currency = currency});
    }

    public Task<Currency> GetByCodeAsync(string code)
    {
        return _mediator.Send(new GetByCode.Query{ Code = code});
    }

    public async Task<int> UpdateAsync(CurrencyDto currencyDTO)
    {
        var currency = _mapper.Map<CurrencyDto, Currency>(currencyDTO);
        return await _mediator.Send(new Update.Command{ CurrencyToUpdate = currency});
    }
}
