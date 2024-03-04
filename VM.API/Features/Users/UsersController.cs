using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.API.Common.Base;

namespace VM.API.Features.Users
{
    public class UsersController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(Create.CreateUserResult), 200)]
        public async Task<IActionResult> Create(Create.CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("SetPassword")]
        [ProducesResponseType(typeof(Create.CreateUserResult), 200)]
        public async Task<IActionResult> SetPassword(SetPassword.SetPasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("VerifyEmail")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> VerifyEmail(VerifyEmail.VerifyEmailCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Modify([FromRoute] int id, [FromBody] Modify.ModifyUserCommand command)
        {
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Delete.DeleteUserCommand command = new();
            command.SetId(id);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAll.GetAllUserResult>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAll.GetAllUserQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetById.GetUserByIdResult), 200)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetById.GetUserByIdQuery();
            query.SetId(id);
            return Ok(await _mediator.Send(query));
        }
    }
}
