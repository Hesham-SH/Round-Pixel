using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs;
public class ItemToUpdateDTO
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}
