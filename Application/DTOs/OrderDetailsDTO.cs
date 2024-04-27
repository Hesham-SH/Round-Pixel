namespace Application.DTOs;

public class OrderDetailsDTO : BaseDTO
{
    public decimal ItemPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid ItemId { get; set; }
    public Guid OrderId { get; set; }
}
