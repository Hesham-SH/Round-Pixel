using API.DTOs;
using Application.DTOs;
using Application.Interfaces;
using Application.Items;
using AutoMapper;
using Domain;

namespace Application.Services;

public class ItemsService : BaseService , IItemsService 
{
    private readonly IMapper _mapper;
    
    public ItemsService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<List<ItemDTO>> GetAllAsync()
    {
        var items = await _mediator.Send(new ListAll.Query());
        var itemsDTOs = _mapper.Map<List<Item>, List<ItemDTO>>(items);
        return itemsDTOs;
    }
    public async Task<ItemDTO> GetByIdAsync(Guid id)
    {
        var item = await _mediator.Send(new GetById.Query{Id = id});
        var itemDTO = _mapper.Map<Item, ItemDTO>(item);
        return itemDTO;
    }
    public async Task<string> Add(ItemDTO itemDTO)
    {
        var item = _mapper.Map<ItemDTO, Item>(itemDTO);
        var result = await _mediator.Send(new Add.Command{Item = item});
        if(result > 0) return "Item Has Been Added Successfully";
        return "Failed To Add Item To The Database";
    }

    public async Task<string> Delete(Guid id)
    {
        var result = await _mediator.Send(new Delete.Command{Id = id});
        if(result > 0) return "Item Has Been Deleted Successfully";
        return "Failed To Delete Item From The Database";
    }

    public async Task<string> UpdateItemQuantityByIdAsync(ItemToUpdateDTO itemToUpdateDTO)
    {
        var result = await _mediator.Send(new UpdateItemQuantity.Command{ ItemToUpdateDTO = itemToUpdateDTO});
        if(result > 0) return "Item's Quantity Has Been Updated Successfully !";
        return $"Failed To Update Item's Quantity In The Database";
    }

    public async Task<string> UndeleteItemByIdAsync(Guid id)
    {
        var result = await _mediator.Send(new Undelete.Command{ Id = id});
        if(result > 0) return "Item Has Been Restored From Trash Successfully";
        return "Failed To Restore Item From Trash";
    }

}
