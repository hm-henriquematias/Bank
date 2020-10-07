using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Business.Application.Commands.Requests;
using Bank.Business.Application.Commands.Responses;
using Bank.Business.Application.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferResponse>>> Get([FromQuery] AccountTransferQuery customerAccount)
        {
            AccountResponse transferResponse = await _mediator.Send(customerAccount);

            ActionResult actionResult;

            if (transferResponse.Status)
                actionResult = Ok(transferResponse);
            else
                actionResult = BadRequest(transferResponse);

            return actionResult;
        }

        [HttpPost]
        public async Task<ActionResult<TransferResponse>> Post([FromBody] TransferRequest transfer)
        {

            TransferResponse transferResponse = await _mediator.Send(transfer);

            ActionResult actionResult;

            if (transferResponse.Status)
                actionResult = Ok(transferResponse);
            else
                actionResult = BadRequest(transferResponse);

            return Ok(actionResult);
        }
    }
}
