using GymLedger.Domains.Api;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Views.Areas.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymLedger.Api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("accountdetails")]
        public IHttpActionResult GetAccountDetails()
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.AccountDetailsGetApiQueryHandlerApi(new Domains.Api.Querys.AccountDetailsGetApiQuery(token));
                var response = handler.Get();

                return Ok(new
                {
                    Status = "Success",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpGet]
        [Route("previouslogins")]
        public IHttpActionResult GetPreviousLogins()
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.GetPreviousLoginsQueryHandlerApi(new Domains.Api.Querys.GetPreviousLoginsQueryApi(token));
                var response = handler.Get();

                return Ok(new
                {
                    Status = "Success",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpDelete]
        [Route("deleteaccount")]
        public IHttpActionResult DeleteAccount(DeleteUserApiView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.DeleteUserCommandHandlerApi(new Domains.Api.Commands.DeleteUserCommandApi(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Data = response.Data,
                    Message = response.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpPut]
        [Route("changepassword")]
        public IHttpActionResult ChangePassword(ChangePasswordView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.ChangePasswordCommandHandlerApi(new Domains.Api.Commands.ChangePasswordCommandApi(token, view));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Data = response.Data,
                    Message = response.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }
    }
}
