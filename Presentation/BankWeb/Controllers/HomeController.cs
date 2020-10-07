using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Business.Application.Commands.Requests;
using Bank.Business.Application.Commands.Responses;
using Bank.Business.Application.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankWeb.Controllers
{
    public class HomeController : ControllerBase
    {
        public string Index()
        {
            return "index";
        }
    }
}
