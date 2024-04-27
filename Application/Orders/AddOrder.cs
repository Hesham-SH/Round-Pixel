using Domain;
using MediatR;
using Persistence;

namespace Application.Orders;

public class AddOrder
{
    public class Command : IRequest<int>
    {
        public Order Order { get; set; }
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
            _context.Orders.Add(request.Order);
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}
