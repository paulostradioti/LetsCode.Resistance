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
    public class RebelController : CustomControllerBase
    {
        private readonly IRebelService _service;
        private readonly IMapper _mapper;

        public RebelController(IRebelService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lists All Rebels",
            Description = "Lists All Rebels",
            OperationId = "Rebel.Get",
            Tags = new[] { "Rebel" })
        ]
        public async Task<IActionResult> Get()
        {
            var rebels = await _service.GetAllAsync();
            return Ok(rebels);
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Get Rebel by his or her ID",
            Description = "Returns the details of a single rebel by his or her ID",
            OperationId = "Rebel.GetOne",
            Tags = new[] { "Rebel" })
        ]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var rebel = await _service.GetById(id);

            if (rebel == null)
                return NotFound();

            return Ok(rebel);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Rebel",
            Description = "Creates a new Rebel",
            OperationId = "Rebel.Post",
            Tags = new[] { "Rebel" })
        ]
        public async Task<IActionResult> Post([FromBody] RebelCreateRequestModel request)
        {
            var entity = _mapper.Map<Rebel>(request);
            var rebel = await _service.CreateAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = rebel.Id }, rebel);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Updates a Rebel Information",
            Description = "Updates the Rebel information",
            OperationId = "Rebel.Put",
            Tags = new[] { "Rebel" })
        ]
        public async Task<IActionResult> Put(Guid id, [FromBody] RebelUpdateRequestModel request)
        {
            var entity = _mapper.Map<Rebel>(request);

            entity.Id = id;
            var rebel = await _service.PutAsync(entity);

            if (rebel == null)
                return NotFound();

            return CreatedAtAction(nameof(Get), new { id = rebel.Id }, rebel);
        }

        [HttpPatch("{id:guid}/Location")]
        [SwaggerOperation(
            Summary = "Updates a Rebel's Location",
            Description = "Updates the last know Location of a Rebel",
            OperationId = "Rebel.Patch",
            Tags = new[] { "Rebel" })
        ]
        public async Task<IActionResult> UpdateLocation(Guid id, [FromBody] LocationUpdateRequestModel request)
        {
            var entity = _mapper.Map<Location>(request);
            var rebel = await _service.UpdateRebelLocationAsync(request.RebelId, entity);

            if (rebel == null)
                return NotFound();

            return CreatedAtAction(nameof(Get), new { id = rebel.Id }, rebel);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(
            Summary = "Deletes a Rebel by his or her ID",
            Description = "Deletes a Rebel by his or her ID",
            OperationId = "Rebel.Delete",
            Tags = new[] { "Rebel" })
        ]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteByIdAsync(id);
            if (success)
                return Ok();

            return NotFound();
        }

        [HttpPost("{id:guid}/Report")]
        [SwaggerOperation(
            Summary = "Reports a Rebel as a Traitor",
            Description = "Reports a Rebel as a Traitor",
            OperationId = "Rebel.ReportAsync",
            Tags = new[] { "Rebel" })
        ]
        public async Task<IActionResult> Post([FromRoute] Guid id)
        {
            var rebel = await _service.ReportAsync(id);

            if (rebel == null)
                return NotFound();

            return Ok();
        }
    }
}