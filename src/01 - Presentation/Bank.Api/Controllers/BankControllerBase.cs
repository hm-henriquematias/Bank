using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using System.Threading.Tasks;

namespace BankWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BankControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BankControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> SendRequestToApplication<TResponse>(IRequest<Result<TResponse>> request) where TResponse : class
        {
            IActionResult actionResult;

            var response = await _mediator.Send(request).ConfigureAwait(false);

            switch (response.IsSuccess)
            {
                case true:
                    actionResult = Ok(response.Value);
                    break;
                case false:
                    actionResult = BadRequest(response.Exception);
                    break;
            }

            return actionResult;
        }
    }
}
