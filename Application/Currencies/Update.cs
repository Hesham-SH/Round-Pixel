using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Currencies
{
    public class Update
    {
        public class Command : IRequest<int>
    {
        public Currency CurrencyToUpdate { get; set; }
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
            var result = await _context.Currencies.FindAsync(request.CurrencyToUpdate.Id);
            result.ExchangeRate = request.CurrencyToUpdate.ExchangeRate;
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
    }
}