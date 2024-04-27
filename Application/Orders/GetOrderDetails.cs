using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders;

public class GetOrderDetails
{
    public class Query : IRequest<List<OrderDetails>>
    {
        public Guid OrderId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<OrderDetails>>
    {
        private readonly ApplicationDatabaseContext _context;
        public Handler(ApplicationDatabaseContext context)
        {
            _context = context; 
        }

        public async Task<List<OrderDetails>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.OrderDetails.Where(O => O.OrderId == request.OrderId).ToListAsync();
        }
    }
}
