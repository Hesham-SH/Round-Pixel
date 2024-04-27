namespace Domain;

public class Order : BaseModel
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
    public virtual AppUser Customer { get; set; }
    public virtual ICollection<OrderDetails> OrderDetails {get; set;}
}
