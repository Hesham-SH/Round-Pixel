using Domain;
using MediatR;
using Persistence;

namespace Application.Items;

public class UpdateItemsAvailableQuantity
{
    public class Command : IRequest<int>
    {
        public List<Item> Items { get; set; }
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
            foreach(var item in request.Items)
            {
               var resultItem = await _context.Items.FindAsync(item.Id);
               resultItem.Quantity = resultItem.Quantity - item.Quantity;
            }
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}
