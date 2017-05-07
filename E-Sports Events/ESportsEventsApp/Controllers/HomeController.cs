﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESportsEventsApp.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        [Route("~/")]
        [Route("")]
        [Route("all")]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}