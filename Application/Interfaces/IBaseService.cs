using MediatR;

namespace Application.Interfaces;

public interface IBaseService
{
    public void AssociateMediatorConnection(IMediator mediator);
}
