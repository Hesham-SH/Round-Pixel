namespace Domain.Interfaces;

public interface ISoftDeleteable
{
    public bool IsDeleted { get; set; }      
    public DateTime DeletedDateTime { get; set; }
    public void Delete()
    {
        IsDeleted = true;
        DeletedDateTime = DateTime.Now;
    }
    public void UndoDelete()
    {
        IsDeleted = false;
    }     
}
