using GymLedger.Domains.Account;
using GymLedger.Domains.Account.Commands;
using GymLedger.Views.Account.Login;
using GymLedger.Views.Account.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymLedger.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public JsonResult Register(RegisterAccountView view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = AccountFactory.RegisterAccountCommandHandler(new RegisterAccountCommand(view));
                    var response = handler.Execute();

                    return Json(new {success = true, responseText = $"{response.Message}", responseReload = true}, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to register account" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public JsonResult Login(LoginAccountView view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = AccountFactory.LoginAccountCommandHandler(new LoginAccountCommand(view));
                    var response = handler.Execute();

                    return Json(new { success = true, responseText = $"{response.Message}", responseReload = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Failed to login account" }, JsonRequestBehavior.AllowGet);
        }
    }
}