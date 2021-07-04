﻿using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System;
using LetsCode.Resistance.Infrastructure.RequestModels;

namespace LetsCode.Resistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RebelController : ControllerBase
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
        public void Put(Guid id, [FromBody] RebelCreateRequestModel value)
        {
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
    }
}