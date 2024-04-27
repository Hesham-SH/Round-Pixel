using Application.Interfaces;
using MediatR;

namespace Application.Services;

public class BaseService : IBaseService
{
    protected IMediator _mediator;
    public BaseService()
    {    
    }

    public void AssociateMediatorConnection(IMediator mediator)
    {
        _mediator = mediator;
    }
}
