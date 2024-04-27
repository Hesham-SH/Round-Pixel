using Domain;
using MediatR;
using Persistence;

namespace Application.Items;

public class Add
{
    public class Command : IRequest<int>
    {
        public Item Item { get; set; }
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
            _context.Items.Add(request.Item);
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}
