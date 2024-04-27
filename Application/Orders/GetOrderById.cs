using Domain;
using MediatR;
using Persistence;

namespace Application.Orders;

public class GetOrderById
{
    public class Query : IRequest<Order>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Order>
    {
        private readonly ApplicationDatabaseContext _context;
        public Handler(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Orders.FindAsync(request.Id);
        }
    }
}
