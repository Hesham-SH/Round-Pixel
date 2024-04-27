using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.DTOs
{
    public class ItemDTO : BaseDTO
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DeletedDateTime { get; set; }
        public Guid UnitOfMeasurement { get; set; }
        public string UnitOfMeasurementName { get; set; }
    }
}