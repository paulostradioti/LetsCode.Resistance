using AutoMapper;
using LetsCode.Resistance.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using LetsCode.Resistance.Infrastructure.RequestModel;
using LetsCode.Resistance.Infrastructure.Service.Interface;

namespace LetsCode.Resistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : CustomControllerBase
    {
        private readonly ITradeService _service;

        public TradeController(ITradeService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a Trade",
            Description = "Trade Inventory Items between rebels.",
            OperationId = "Trade.Post",
            Tags = new[] { "Trade" })
        ]
        public async Task<IActionResult> Post([FromBody] TradeRequestModel request)
        {
            try
            {
                await _service.Trade(request);
            }
            catch (TradeException e)
            {
                return BadRequest(new { e.Message });
            }
            catch (Exception e)
            {
                return InternalServerError(new { e.Message });
            }

            return Ok();
        }
    }
}