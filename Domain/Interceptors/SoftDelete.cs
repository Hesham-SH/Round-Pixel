using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Domain.Interceptors;

public class SoftDelete : SaveChangesInterceptor
{
    private readonly ILogger<SoftDelete> _logger;
    public SoftDelete(ILogger<SoftDelete> logger)
    {
        _logger = logger;
    }
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if(eventData.Context is null)
            return result;
        
        var entriesToDelete =  eventData.Context.ChangeTracker.Entries()
        .Where(entry => entry.State == EntityState.Deleted && entry.Entity is ISoftDeleteable)
        .ToList();

        foreach(var entry in entriesToDelete)
        {
            try
            {
                entry.State = EntityState.Modified;
                ((ISoftDeleteable)entry.Entity).Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured During Deleting An Item");
            }
            finally
            {
                eventData.Context.ChangeTracker.Entries().ToList().Remove(entry);
            }
        }
        return result;
    }
}
