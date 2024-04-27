using Domain.Interfaces;

namespace Domain;

public class Item : BaseModel , ISoftDeleteable
{
    public string ItemName { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }      
    public DateTime DeletedDateTime { get; set; }
    public Guid UnitOfMeasurementId { get; set; }
    public virtual UnitOfMeasurement UnitOfMeasurement {get; set;}
    public virtual ICollection<OrderDetails> OrderDetails {get; set;}
}
