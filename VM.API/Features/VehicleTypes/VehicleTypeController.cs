using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.API.Common.Base;

namespace VM.API.Features.VehicleTypes
{
    public class VehicleTypeController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(Create.CreateVehicleTypeResult), 200)]
        public async Task<IActionResult> Create(Create.CreateVehicleTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Modify([FromRoute] int id, [FromBody] Modify.ModifyVehicleTypeCommand command)
        {
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Delete.DeleteVehicleTypeCommand command = new();
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAll.GetAllVehicleTypeResult>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAll.GetAllVehicleTypeQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetById.GetVehicleTypeByIdResult), 200)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetById.GetVehicleTypeByIdQuery();
            query.SetId(id);
            return Ok(await _mediator.Send(query));
        }
    }
}
