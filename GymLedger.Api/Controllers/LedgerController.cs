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

        [HttpGet]
        [Route("getexercise")]
        public IHttpActionResult GetExercise(string uniqueId)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.GetExerciseQueryHandlerApi(new GetExerciseQueryApi(token, uniqueId));
                var response = handler.Get();

                return Ok(new
                {
                    response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpPost]
        [Route("addexercise")]
        public IHttpActionResult AddExercise(AddExerciseView view) 
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.AddExerciseCommandHandlerApi(new Domains.Api.Commands.AddExerciseCommand(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Added Exercise",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpPut]
        [Route("editexercise")]
        public IHttpActionResult EditExercise(EditExerciseView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.EditExerciseCommandHandlerApi(new Domains.Api.Commands.EditExerciseCommandApi(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Edited Exercise",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpDelete]
        [Route("deleteexercise")]
        public IHttpActionResult DeleteExercise(DeleteExerciseView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.DeleteExerciseCommandHandlerApi(new Domains.Api.Commands.DeleteExerciseCommandApi(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Deleted Exercise",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpGet]
        [Route("getsessions")]
        public IHttpActionResult GetSessions()
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.GetSessionsQueryHandlerApi(new Domains.Api.Querys.GetSessionsQueryApi(token));
                var response = handler.Get();

                return Ok(new
                {
                    total = response.Sessions.Count,
                    rows = response.Sessions
                    
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpGet]
        [Route("getsession")]
        public IHttpActionResult GetSession(string uniqueId)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.GetSessionQueryHandlerApi(new Domains.Api.Querys.GetSessionQueryApi(token, uniqueId));
                var response = handler.Get();

                return Ok(new
                {
                    rows = response

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpPost]
        [Route("addsession")]
        public IHttpActionResult AddSession(AddSessionApiView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.AddSessionCommandHandlerApi(new Domains.Api.Commands.AddSessionCommandApi(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Added Session",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpPut]
        [Route("editsession")]
        public IHttpActionResult EditSession(EditSessionView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.EditSessionCommandHandlerApi(new Domains.Api.Commands.EditSessionCommandApi(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Edited Session",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpDelete]
        [Route("deletesession")]
        public IHttpActionResult DeleteSession(DeleteSessionView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.DeleteSessionCommandHandlerApi(new Domains.Api.Commands.DeleteSessionCommandApi(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Deleted Session",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpGet]
        [Route("onerepmaxes")]
        public IHttpActionResult GetOneRepMaxes()
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.GetOneRepMaxesQueryHandlerApi(new Domains.Api.Querys.GetOneRepMaxesQueryApi(token));
                var response = handler.Get();

                return Ok(new
                {
                    Data = response.OneRepMaxes

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpPost]
        [Route("addonerepmax")]
        public IHttpActionResult AddOneRepMax(AddOneRepMaxApiView view)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.AddOneRepMaxCommandHandlerApi(new Domains.Api.Commands.AddOneRepMaxCommandApi(view, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Added One Rep Max",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpDelete]
        [Route("deleteonerepmax")]
        public IHttpActionResult DeleteOneRepMax(string uniqueId)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.DeleteOneRepMaxCommandHandlerApi(new Domains.Api.Commands.DeleteOneRepMaxCommandApi(uniqueId, token));
                var response = handler.Execute();

                return Ok(new
                {
                    Status = "Success",
                    Message = "Successfully Deleted One Rep Max",
                    response.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }

        [HttpGet]
        [Route("onerepmax")]
        public IHttpActionResult GetOneRepMax(string uniqueId)
        {
            var token = Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                return Unauthorized(); // Token missing
            }

            try
            {
                var handler = ApiFactory.GetOneRepMaxQueryHandlerApi(new Domains.Api.Querys.GetOneRepMaxQueryApi(token, uniqueId));
                var response = handler.Get();

                return Ok(new
                {
                    Data = response

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Invalid token or error handling
            }
        }
    }
}
