namespace Domain;

public class OrderDetails : BaseModel
{
    public decimal ItemPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid ItemId { get; set; }
    public virtual Item Item { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
}
