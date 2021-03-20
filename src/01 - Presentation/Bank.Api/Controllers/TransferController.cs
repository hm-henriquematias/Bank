using Bank.Business.Application.Commands.Requests;
using Bank.Business.Application.Dto;
using Bank.Business.Application.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using System.Threading.Tasks;

namespace BankWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : BankControllerBase
    {
        public TransferController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AccountTransferQuery customerAccount)
            => await SendRequestToApplication(customerAccount).ConfigureAwait(false);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransferDto transfer)
        {
            var amountTransfersFromCurrentMonth = await _mediator.Send(new AmountAccountMonthlyTransferQuery(transfer.From)).ConfigureAwait(false);

            Result<string> result;
            if (amountTransfersFromCurrentMonth.IsSuccess)
            {
                var transferTax = await _mediator.Send(new FarePlanTransferTaxQuery(transfer.From, amountTransfersFromCurrentMonth.Value)).ConfigureAwait(false);
                if (transferTax.IsSuccess)
                {
                    var transferResult = await _mediator.Send(new TransferCommandRequest(transfer, transferTax.Value)).ConfigureAwait(false);
                    result = (transferResult.IsSuccess) ? Result.Success(transferResult.Value) : Result.Error<string>(transferResult.Exception);
                }
                else
                {
                    result = Result.Error<string>(transferTax.Exception);
                }

            }
            else
            {
                result = Result.Error<string>(amountTransfersFromCurrentMonth.Exception);
            }

            IActionResult response;

            switch (result.IsSuccess)
            {
                case true:
                    response = Ok(result.Value);
                    break;
                case false:
                    response = BadRequest(result.Exception);
                    break;
            }

            return response;

        }
    }
}
