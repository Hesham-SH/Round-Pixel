using API.DTOs;
using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IItemsService : IBaseService
{

    //Get Items By Order Id

    Task<List<ItemDTO>> GetAllAsync();
    Task<ItemDTO> GetByIdAsync(Guid id);
    Task<string> Delete(Guid id);
    Task<string> Add(ItemDTO itemDTO);
    Task<string> UpdateItemQuantityByIdAsync(ItemToUpdateDTO itemToUpdateDTO);
    Task<string> UndeleteItemByIdAsync(Guid id);
}
