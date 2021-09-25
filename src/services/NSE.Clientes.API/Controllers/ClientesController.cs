using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClientesController(IMediatorHandler mediatorHandler)
        {
            this._mediatorHandler = mediatorHandler;

        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var result = await this._mediatorHandler.SendCommand(
                new RegistrarClienteCommand(Guid.NewGuid(), "Francisco", "fcobarbosa@hotmail.com.br", "30314299076"));
            if (!result.IsValid) CustomResponse(result);
            return View(result);
        }
    }
}
