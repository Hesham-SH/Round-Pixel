namespace Domain;
public class UnitOfMeasurement : BaseModel
{  
    public string UOM { get; set; }    
    public string Description { get; set; }
    public virtual ICollection<Item> Items {get; set;}   
}