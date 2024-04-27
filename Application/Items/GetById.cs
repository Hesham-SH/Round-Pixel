using Domain;
using MediatR;
using Persistence;

namespace Application.Items;

public class GetById
{
    public class Query : IRequest<Item> 
    { 
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Item>
    {
        private readonly ApplicationDatabaseContext _context;

        public Handler(ApplicationDatabaseContext context)
        {
            _context = context; 
        }

        public async Task<Item> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Items.FindAsync(request.Id);
        }
    }
}
