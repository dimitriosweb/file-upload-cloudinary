using EFStories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFStories.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BackgroundUploader bgUploader = GetUploader();

            if (bgUploader.Progress < 100)
                return PartialView("Upload", bgUploader);

            // The whole uploader object will be passed to view as model
            // This is to show usage of some API helpers that builds HTML or URL
            return PartialView("Show", bgUploader);
        }

        public ActionResult Progress()
        {
            BackgroundUploader bgUploader = GetUploader();

            return Json(new { progress = bgUploader.Progress }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets uploader from session or creates a new one
        /// </summary>
        private BackgroundUploader GetUploader()
        {
            BackgroundUploader bgUploader = Session["bguploader"] as BackgroundUploader;

            if (bgUploader == null)
            {
                bgUploader = new BackgroundUploader();
                Session.Add("bguploader", bgUploader);
                bgUploader.Upload();
                return bgUploader;
            }

            return bgUploader;
        }

    }
}