using MediatR;
using Persistence;

namespace Application.Orders;

public class DeleteOrder
{
    public class Command : IRequest<int>
    {
        public Guid Id { get; set; }
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
            var order = await _context.Orders.FindAsync(request.Id);
            _context.Orders.Remove(order);
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}
