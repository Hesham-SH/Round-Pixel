namespace Domain;
public class Currency : BaseModel
{
    public string Code { get; set; }
    public decimal ExchangeRate { get; set; }
}
