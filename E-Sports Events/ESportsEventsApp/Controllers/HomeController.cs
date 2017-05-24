using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ESportsEventsApp.Controllers
{
    using System;

    using AutoMapper;

    using Data;

    using global::Models.Images;
    using global::Models.Videos;

    using Helpers;

    using ViewModels;

    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        [Route("~/")]
        [Route("")]
        [Route("all")]
        [Route("index")]
        public ActionResult Index()
        {
            var rnd = new Random();
            var eventImages =
                db.Events.OrderBy(e => e.StartDate).Take(Constants.LatestEventsCount).SelectMany(e => e.EventImages).ToList().Select(ei=>new { key = rnd.Next(), ei}).OrderBy(ei => ei.key).Select(i=>i.ei).Take(Constants.LatestImagesCount);
            var eventImagesModel = Mapper.Map<IEnumerable<EventImage>, IEnumerable<EventImageViewModel>>(eventImages);
            var eventVideos =
               db.Events.OrderBy(e => e.StartDate).Take(Constants.LatestEventsCount).SelectMany(e => e.EventVideos).ToList().Select(ei => new { key = rnd.Next(), ei }).OrderBy(ei => ei.key).Select(i => i.ei).Take(Constants.LatestVideosCount);
            var eventVideosModel = Mapper.Map<IEnumerable<EventVideo>, IEnumerable<EventVideoViewModel>>(eventVideos);
            var model = new HomePageViewModel
                            {
                                LatestEventImages = eventImagesModel,
                                LatestEventVideos = eventVideosModel
                            };
            return View(model);
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