using API.DTOs;
using MediatR;
using Persistence;

namespace Application.Items;

public class UpdateItemQuantity
{
    public class Command : IRequest<int>
    {
        public ItemToUpdateDTO ItemToUpdateDTO { get; set; }
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
            var result = await _context.Items.FindAsync(request.ItemToUpdateDTO.Id);
            result.Quantity = request.ItemToUpdateDTO.Quantity;
            if(await _context.SaveChangesAsync() > 0) return 1;
            return 0;
        }
    }
}
