using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.Areas.Ledger;
using GymLedger.Domains.Areas.Profile;
using GymLedger.Views.Areas.Ledger;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymLedger.Views.Areas.Profile;
using GymLedger.Domains.Areas.Profile.Commands;

namespace GymLedger.Web.Controllers
{
    [CustomAuthorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public PartialViewResult AccountDetails()
        {
            var handler = ProfileFactory.AccountDetailsGetQueryHandler(new Domains.Areas.Profile.Querys.AccountDetailsGetQuery(this.HttpContext));
            var response = handler.Get();
            return PartialView("_AccountDetails", response);
        }

        [HttpGet]
        public PartialViewResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }

        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordView view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = ProfileFactory.ChangePasswordCommandHandler(new ChangePasswordCommand(this.HttpContext, view));
                    var response = handler.Execute();

                    return Json(new { success = true, responseText = $"{response.Message}", responseReload = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to change password" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult DeleteAccount()
        {
            return PartialView("_DeleteAccount");
        }

        [HttpPost]
        public JsonResult DeleteAccount(DeleteUserView view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = ProfileFactory.DeleteUserCommandHandler(new DeleteUserCommand(this.HttpContext, view));
                    var response = handler.Execute();

                    return Json(new { success = true, responseText = $"{response.Message}", responseReload = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to change password" }, JsonRequestBehavior.AllowGet);
        }
    }
}