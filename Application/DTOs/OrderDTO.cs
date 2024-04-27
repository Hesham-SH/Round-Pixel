namespace Application.DTOs;
public class OrderDTO : BaseDTO
{
    public DateTimeOffset RequestDate { get; set; }
    public DateTimeOffset CloseDate { get; set; }
    public string Status { get; set; }
    public string DiscountPromoCode { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal ExchangeRate { get; set; }
    public decimal ForeignPrice { get; set; }
    public string CurrencyCode { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName {get; set;}
    public ICollection<ItemDTO> Items {get; set;}
}