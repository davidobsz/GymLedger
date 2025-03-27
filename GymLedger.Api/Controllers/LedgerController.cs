using GymLedger.Views.Areas.Ledger;
using System;
using System.Web.Http;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.Areas.Ledger;
using GymLedger.Domains.Api;

namespace GymLedger.Api.Controllers
{
    [RoutePrefix("api/ledger")]
    public class LedgerController : ApiController
    {
        [HttpGet]
        [Route("getexercises")]
        public IHttpActionResult GetExercises()
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.GetExercisesQueryHandlerApi(new GetExercisesQueryApi(token));
                var response = handler.Get();

                return Ok(new
                {
                    total = response.Exercises.Count,
                    rows = response.Exercises
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }
    }
}
