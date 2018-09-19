using CloudinaryDotNet;
using EFStories.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFStories.Controllers
{
    public class HomeController : Controller
    {
        static Cloudinary m_cloudinary;
        public HomeController()
        {
            Account acc = new Account(
            Properties.Settings.Default.CloudName,
            Properties.Settings.Default.ApiKey,
            Properties.Settings.Default.ApiSecret);

            m_cloudinary = new Cloudinary(acc);
        }

        public ActionResult Index()
        {
            BackgroundUploader bgUploader = GetUploader();

            if (bgUploader.Progress < 100)
                return PartialView("Upload", bgUploader);

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
                bgUploader = new BackgroundUploader(m_cloudinary);
                Session.Add("bguploader", bgUploader);
                bgUploader.Upload();
                return bgUploader;
            }

            return bgUploader;
        }

    }
}