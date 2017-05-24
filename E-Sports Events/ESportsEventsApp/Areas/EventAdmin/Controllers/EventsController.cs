namespace ESportsEventsApp.Areas.EventAdmin.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using global::Models;
    using global::Models.Images;
    using global::Models.Videos;

    using Microsoft.AspNet.Identity;

    using ViewModels;

    using Constants = Helpers.Constants;

    [Authorize(Roles = "EventAdmin")]
    [RouteArea("EventAdmin", AreaPrefix = "eventadmin")]
    [RoutePrefix("events")]
    public class EventsController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: EventAdmin/Events
        [HttpGet]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index()
        {
            var userId = this.GetCurrentUserId();
            var events = this.db.Events.ToList().Where(e=>e.EventAdmins.Any(ea=>ea.Id == userId));
            var model = Mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            return this.View(model);
        }

        // GET: EventAdmin/Events/Details/5
        [HttpGet]
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }
            var model = Mapper.Map<Event, EventAllDetailsViewModel>(@event);
            return this.View(model);
        }

        // GET: EventAdmin/Events/Edit/5
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<Event, EventBindingModel>(@event);
            //ViewBag.Id = new SelectList(db.Logos, "Id", "Caption", @event.Id);
            return this.View(model);
        }

        // POST: EventAdmin/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Name, Location, PrizePool, Description, TierType, StartDate, EndDate, Logo")] EventBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var @event = this.db.Events.Find(model.Id);
                var logo = @event.Logo;
                var hasUrl = !string.IsNullOrEmpty(model.Logo.Url);
                if (hasUrl)
                {
                    if (logo != null)
                    {
                        this.db.Logos.Remove(logo);
                    }

                    model.Logo.Url = Constants.LogosFolderPath + model.Logo.Url;
                    logo = Mapper.Map<LogoBindingModel, Logo>(model.Logo);
                    this.db.Entry(logo).State = EntityState.Added;
                    @event.Logo = logo;
                }

                @event.Name = model.Name;
                @event.Location = model.Location;
                @event.PrizePool = model.PrizePool;
                @event.TierType = model.TierType;
                @event.StartDate = model.StartDate;
                @event.EndDate = model.EndDate;
                @event.Description = model.Description;
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [Route("addeventimage/{id}")]
        public ActionResult AddEventImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }

            ViewBag.EventName = @event.Name; 
            return this.View();
        }

        [HttpPost]
        [Route("addeventimage/{id}")]
        public ActionResult AddEventImage(int? id, EventImageBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(id);
                bind.Url = Constants.EventImagesFolderPath + bind.Url;
                if (!@event.EventImages.Any(ei => ei.Url == bind.Url))
                {
                    var image = Mapper.Map<EventImageBindingModel, EventImage>(bind);
                    @event.EventImages.Add(image);
                    this.db.SaveChanges();
                }

                return this.RedirectToAction("AddEventImage");

            }
            return this.View(bind);
        }

        [HttpGet]
        [Route("addeventimagelocal/{id}")]
        public ActionResult AddEventImageLocal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }

            ViewBag.EventName = @event.Name;
            return this.View();
        }

        [HttpPost]
        [Route("addeventimagelocal/{id}")]
        public ActionResult AddEventImageLocal(int? id, EventImageBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                var photo = Request.Files["Url"];
                if (photo == null)
                {
                    return this.View();
                }

                var directory = $"{Server.MapPath("~")}/Content/Images/Event Images/";
                photo.SaveAs(Path.Combine(directory, photo.FileName));
                bind.Url = Constants.EventImagesFolderPath + photo.FileName;
                if (!@event.EventImages.Any(ei => ei.Url == bind.Url))
                {
                    var image = Mapper.Map<EventImageBindingModel, EventImage>(bind);
                    @event.EventImages.Add(image);
                    this.db.SaveChanges();
                }

                ViewBag.EventName = @event.Name;

                return this.RedirectToAction("AddEventImageLocal");
            }
            return this.View();
        }

        [HttpGet]
        [Route("removeeventimage/{id}")]
        public ActionResult RemoveEventImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }

            var addedImages = @event.EventImages;
            if (addedImages.Count == 0)
            {
                return this.RedirectToAction("Index");
            }

            var model = Mapper.Map<Event, RemoveEventImageBindingModel>(@event);
            return this.View(model);
        }

        [HttpPost]
        [Route("removeeventimage/{id}")]
        public ActionResult RemoveEventImage(int? id, RemoveEventImageBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound("No such event has been found.");
                }

                var image = this.db.EventImages.Find(bind.ImageToRemove.Id);
                //image.Url += Constants.EventImagesFolderPath + image.Url;
                if (image != null)
                {
                    var result = this.db.EventImages.Remove(image);
                    this.db.SaveChanges();
                }

                var addedImages = @event.EventImages;
                if (addedImages.Count == 0)
                {
                    return this.RedirectToAction("Index");
                }

                bind = Mapper.Map<Event, RemoveEventImageBindingModel>(@event);
            }
            return this.View(bind);
        }

        [HttpGet]
        [Route("addeventvideo/{id}")]
        public ActionResult AddEventVideo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }

            ViewBag.EventName = @event.Name;
            return this.View();
        }

        [HttpPost]
        [Route("addeventvideo/{id}")]
        public ActionResult AddEventVideo(int? id, EventVideoBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                bind.Url = Constants.YoutubeVideosPath + bind.Url;
                if (@event.EventVideos.All(ei => ei.Url != bind.Url))
                {
                    var video = Mapper.Map<EventVideoBindingModel, EventVideo>(bind);
                    @event.EventVideos.Add(video);
                    this.db.SaveChanges();
                }

                return this.RedirectToAction("AddEventVideo");

            }
            return this.View(bind);
        }

        [HttpGet]
        [Route("removeeventvideo/{id}")]
        public ActionResult RemoveEventVideo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }

            var addedVideos = @event.EventVideos;
            if (addedVideos.Count == 0)
            {
                return this.RedirectToAction("Index");
            }

            var model = Mapper.Map<Event, RemoveEventVideoBindingModel>(@event);
            return this.View(model);
        }

        [HttpPost]
        [Route("removeeventvideo/{id}")]
        public ActionResult RemoveEventVideo(int? id, RemoveEventVideoBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound("No such event has been found.");
                }

                var video = this.db.EventVideos.Find(bind.VideoToRemove.Id);
                if (video != null)
                {
                    var result = this.db.EventVideos.Remove(video);
                    this.db.SaveChanges();
                }

                var addedVideos = @event.EventVideos;
                if (addedVideos.Count == 0)
                {
                    return this.RedirectToAction("Index");
                }

                bind = Mapper.Map<Event, RemoveEventVideoBindingModel>(@event);
            }
            return this.View(bind);
        }

        private string GetCurrentUserId()
        {
            var userId = this.User.Identity.GetUserId();

            return userId;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
