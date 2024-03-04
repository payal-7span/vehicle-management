using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.API.Common.Base;

namespace VM.API.Features.FeesStructures
{
    public class FeesStructuresController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(Create.CreateFeesStructureResult), 200)]
        public async Task<IActionResult> Create(Create.CreateFeesStructureCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Modify([FromRoute] int id, [FromBody] Modify.ModifyFeesStructureCommand command)
        {
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Delete.DeleteFeesStructureCommand command = new();
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAll.GetAllFeesStructureResult>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAll.GetAllFeesStructureQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("by-type-id/{id}")]
        [ProducesResponseType(typeof(GetByTypeId.GetFeesStructureByTypeIdResult), 200)]
        public async Task<IActionResult> GetGyTypeId([FromRoute] int id)
        {
            var query = new GetByTypeId.GetFeesStructureByTypeIdQuery();
            query.SetTypeId(id);
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetById.GetFeesStructureByIdResult), 200)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetById.GetFeesStructureByIdQuery();
            query.SetId(id);
            return Ok(await _mediator.Send(query));
        }
    }
}
