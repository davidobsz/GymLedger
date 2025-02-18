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

namespace GymLedger.Web.Controllers
{
    [CustomAuthorize]
    public class LedgerController : Controller
    {

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
                    var handler = LedgerFactory.AddExerciseCommandHandler(new AddExerciseCommand(view));
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
                    var handler = LedgerFactory.GetExercisesQueryHandler(new GetExercisesQuery());
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
    }
}