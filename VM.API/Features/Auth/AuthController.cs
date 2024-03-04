using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VM.API.Common.Base;

namespace VM.API.Features.Auth
{
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Login.LoginResult), 200)]
        public async Task<IActionResult> Login(Login.LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        //[HttpPost("set-password")]
        //[AllowAnonymous]
        //public async Task<IActionResult> SetPassword(SetPassword.SetPasswordCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}
    }
}
