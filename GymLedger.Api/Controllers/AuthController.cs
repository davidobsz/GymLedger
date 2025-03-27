using GymLedger.Domains.Api;
using GymLedger.Domains.Api.Commands;
using GymLedger.Views.Account.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymLedger.Api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginAccountView view)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request.");
            }

            try
            {
                var handler = ApiFactory.LoginApiAccountCommandHandler(new LoginApiAccountCommand(view));
                var response = handler.Execute();

                return Ok(new
                {
                    token = response.Token,
                    message = response.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
