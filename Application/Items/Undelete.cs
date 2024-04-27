using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using MediatR;
using Persistence;

namespace Application.Items;

public class Undelete
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
            var item = await _context.Items.FindAsync(request.Id);
            if(item is not null)
            {
                if(item is ISoftDeleteable softDeleteable)
                {
                    softDeleteable.UndoDelete();
                }
            }
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}
