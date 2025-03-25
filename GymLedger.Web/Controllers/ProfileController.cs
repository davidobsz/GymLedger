using GymLedger.Domains.Areas.Profile;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}