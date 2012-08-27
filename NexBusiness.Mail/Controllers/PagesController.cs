using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NexBusiness.Mail.Controllers
{
    public class PagesController : Controller
    {
        //
        // GET: /Pages/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Help", new { area = "HelpPage" });
        }

    }
}
