using MediatR;
using Persistence;

namespace Application.Orders;

public class CloseOrder
{
    public class Command : IRequest<int> 
    {
        public Guid OrderId { get; set; }
    }

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly ApplicationDatabaseContext _context;
        public Handler(ApplicationDatabaseContext context)
        {
            _context = context; 
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var orderToClose = await _context.Orders.FindAsync(request.OrderId);
            orderToClose.Status = "Closed";
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}
