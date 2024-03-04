using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.API.Common.Base;

namespace VM.API.Features.FeesHeads
{
    public class FeesHeadsController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(Create.CreateFeesHeadCommand), 200)]
        public async Task<IActionResult> Create(Create.CreateFeesHeadCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Modify([FromRoute] int id, [FromBody] Modify.ModifyFeesHeadCommand command)
        {
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Delete.DeleteFeesHeadCommand command = new();
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAll.GetAllFeesHeadResult>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAll.GetAllFeesHeadQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetById.GetFeesHeadByIdResult), 200)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetById.GetFeesHeadByIdQuery();
            query.SetId(id);
            return Ok(await _mediator.Send(query));
        }
    }
}
