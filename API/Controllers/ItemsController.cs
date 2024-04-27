using API.DTOs;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ItemsController : BaseApiController
{
    private readonly IItemsService _itemsService;

    public ItemsController(IItemsService itemsService)
    {
        _itemsService = itemsService;
        _itemsService.AssociateMediatorConnection(Mediator);
    }

    [Authorize(Policy = "RequireUserRole")]
    [HttpGet]
    public async Task<ActionResult<List<ItemDTO>>> GetAll()
    {
        return await _itemsService.GetAllAsync();
    }

    [Authorize(Policy = "RequireUserRole")]
    [HttpGet("GetById/{id:Guid}")]
    public async Task<ActionResult<ItemDTO>> GetById(Guid id)
    {
        return await _itemsService.GetByIdAsync(id);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost]
    public async Task<ActionResult<string>> Add(ItemDTO itemDTO)
    {
        return await _itemsService.Add(itemDTO);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(Guid id)
    {
        return await _itemsService.Delete(id);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("UpdateItemQuantityById")]
    public async Task<ActionResult<string>>UpdateItemQuantityById(ItemToUpdateDTO itemToUpdateDTO)
    {
        return await _itemsService.UpdateItemQuantityByIdAsync(itemToUpdateDTO);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("RestoreDeleted")]
    public async Task<ActionResult<string>>UndeleteItemById(Guid id)
    {
        return await _itemsService.UndeleteItemByIdAsync(id);
    }
}
