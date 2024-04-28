using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class CurrencyController : BaseApiController
{
    private readonly ICachingService _cachingService;
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICachingService cachingService, 
                            ICurrencyService currencyService)
    {
        _currencyService = currencyService;
        _cachingService = cachingService;
        _currencyService.AssociateMediatorConnection(Mediator);
    }

    [HttpGet("GetCurrencyFromRedis")]
    public  IActionResult GetCurrencyFromRedis([FromQuery] string key)
    {
        var cacheData = _cachingService.GetData<decimal?>(key);
        if(cacheData is not null)
            return Ok(cacheData);

        return NotFound();

    }

    [HttpPost("SetCurrencyInRedis")]
    public IActionResult SetCurrencyInRedis([FromBody] CurrencyDto currencyDto)
    {
        bool isSet	= _cachingService.SetData<decimal>(currencyDto.Code, currencyDto.Exchange); 
        if (!isSet)
            return BadRequest("Error Setting Currency In Cache");

        return Ok(new CurrencyDto{Code = currencyDto.Code, Exchange = currencyDto.Exchange });

    }

    [HttpPost]
    public async Task<ActionResult<string>> Add([FromBody]CurrencyDto currencyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if(await _currencyService.Add(currencyDto) == 0) return BadRequest("Error Adding Currency !");
        _cachingService.SetData<decimal>(currencyDto.Code, currencyDto.Exchange);
        return Ok("Currency Added Successfully");
    }

    [HttpGet("GetCurrencyByCode/{code}")]
    public async Task<ActionResult<Currency>> GetCurrencyByCode(string code)
    {
        var result = await _currencyService.GetByCodeAsync(code);
        if(result is null) return NotFound("Currency Isn't Stored In Database");
        return Ok(result);
    }

    [HttpGet("Update")]
    public async Task<ActionResult<string>> Update(CurrencyDto currencyDTO)
    {
        var result = await _currencyService.UpdateAsync(currencyDTO);
        if(result > 0) return Ok($"Currency : {currencyDTO.Code} Was Updated Successfully !");
        return BadRequest($"Failed To Update Currency : {currencyDTO.Code} In The Database");
    }
}
