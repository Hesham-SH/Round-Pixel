using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application;

public class GetByCode
{
    public class Query : IRequest<Currency>
    {
        public string Code { get; set; }
    }

    public class Handler : IRequestHandler<Query, Currency>
    {
        private readonly ApplicationDatabaseContext _context;
        public Handler(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Currency> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Currencies.FirstAsync(C => C.Code == request.Code);
        }
    }
}
