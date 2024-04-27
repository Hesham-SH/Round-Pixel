using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Currencies;
public class Add
{
    public class Command : IRequest<int>
    {
        public Currency Currency { get; set; }
    }

    public class Handler : IRequestHandler<Command , int>
    {
        private readonly ApplicationDatabaseContext _context;
        public Handler(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Currencies.Add(request.Currency);
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}