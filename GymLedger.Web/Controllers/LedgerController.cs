using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.Account;
using GymLedger.Domains.Areas.Ledger.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GymLedger.Domains.Areas.Ledger;
using GymLedger.Views.Areas.Ledger;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Models.Models;

namespace GymLedger.Web.Controllers
{
    [CustomAuthorize]
    public class LedgerController : Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult AddExercise()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddExercise(AddExerciseView view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = LedgerFactory.AddExerciseCommandHandler(new AddExerciseCommand(view, this.HttpContext));
                    var response = handler.Execute();

                    return Json(new { success = true, responseText = $"{response.Message}", responseReload = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to add exercise" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Exercises()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetExercises()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = LedgerFactory.GetExercisesQueryHandler(new GetExercisesQuery(this.HttpContext));
                    var response = handler.Get();

                    return Json(new { total = response.Exercises.Count, rows = response.Exercises }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to get exercises" }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public PartialViewResult GetExerciseDetails(string uniqueId)
        {
            try
            {
                var handler = LedgerFactory.GetExerciseDetailsQueryHandler(new GetExerciseDetailQuery(this.HttpContext, uniqueId));
                var response = handler.Get();

                return PartialView("_ExerciseDetail",response);
            }
            catch (Exception ex)
            {
                return PartialView("_ExerciseDetail");
            }
        }

        [HttpGet]
        public ActionResult AddSession()
        {
            try
            {
                var handler = LedgerFactory.GetExercisesQueryHandler(new GetExercisesQuery(this.HttpContext));
                var response = handler.Get();

                List<Exercise> exercises = new List<Exercise>();

                foreach (var resp in response.Exercises)
                {
                    Exercise exercise = new Exercise
                    {
                        Name = resp.Name
                    };

                    exercises.Add(exercise);
                }
                var view = new AddSessionView { Exercises = exercises };
                return View(view);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult AddSession(AddSessionView view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = LedgerFactory.AddSessionCommandHandler(new AddSessionCommand(view, this.HttpContext));
                    var response = handler.Execute();

                    return Json(new { success = true, responseText = $"{response.Message}", responseReload = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to add session" }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Sessions()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetSessions()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = LedgerFactory.GetSessionsQueryHandler(new GetSessionsQuery(this.HttpContext));
                    var response = handler.Get();

                    return Json(new { total = response.Sessions.Count, rows = response.Sessions }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to get sessions" }, JsonRequestBehavior.AllowGet);

        }
    }
}