using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders;

public class GetUserOrders
{
    public class Query : IRequest<List<Order>> 
    { 
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<Order>>
    {
        private readonly ApplicationDatabaseContext _context;
        public Handler(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> Handle(Query request, CancellationToken cancellationToken)
        {
            string userId = request.UserId.ToString();
            return await _context.Orders.Where(O => O.CustomerId == userId).ToListAsync();
        }
    }
}
