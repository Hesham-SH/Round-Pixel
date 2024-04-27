using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Items;

public class ListAllDeletedItems
{
    public class Query : IRequest<List<Item>> { }

    public class Handler : IRequestHandler<Query, List<Item>>
    {
        private readonly ApplicationDatabaseContext _context;
        public Handler(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Items.Where(I => I.IsDeleted == true).ToListAsync();
        }
    }
}
