namespace ESportsEventsApp.Areas.EventAdmin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using global::Models;
    using global::Models.Images;

    using Microsoft.AspNet.Identity;

    using ViewModels;

    using Constants = Helpers.Constants;

    [RouteArea("EventAdmin", AreaPrefix = "eventadmin")]
    [RoutePrefix("events")]
    public class EventsController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        public EventsController()
        {
        }

        // GET: EventAdmin/Events
        [HttpGet]
        [Authorize(Roles = "EventAdmin")]
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
        [Authorize(Roles = "EventAdmin")]
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
            var model = Mapper.Map<Event, EventDetailsViewModel>(@event);
            return this.View(model);
        }

        // GET: EventAdmin/Events/Edit/5
        [HttpGet]
        [Authorize(Roles = "EventAdmin")]
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
        [Authorize(Roles = "EventAdmin")]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Name, Location, PrizePool, Description, TierType, StartDate, EndDate, Logo")] EventBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(model.Id);
                var logo = @event.Logo;
                if (logo != null)
                {
                    this.db.Logos.Remove(logo);
                }
                //this.db.SaveChanges();
                //var @event = Mapper.Map<EventBindingModel, Event>(model);
                model.Logo.Url = Constants.ImagesFolderPath + model.Logo.Url;
                logo = Mapper.Map<LogoBindingModel, Logo>(model.Logo);
                this.db.Entry(logo).State = EntityState.Added;
                @event.Logo = logo;
                @event.Name = model.Name;
                @event.Location = model.Location;
                @event.PrizePool = model.PrizePool;
                @event.TierType = model.TierType;
                @event.StartDate = model.StartDate;
                @event.EndDate = model.EndDate;
                @event.Description = model.Description;
                this.db.Entry(@event).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            //ViewBag.Id = new SelectList(db.Logos, "Id", "Caption", @event.Id);
            return this.View(model);
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
