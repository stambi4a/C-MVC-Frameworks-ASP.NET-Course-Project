namespace ESportsEventsApp.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using Extensions;

    using global::Models;
    using global::Models.Images;

    using ViewModels;

    using Constants = Helpers.Constants;

    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("events")]
    public class EventsController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: Admin/Events
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index()
        {
            var events = db.Events.ToList();
            var model = Mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            return View(model);
        }

        // GET: Admin/Events/Details/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<Event, EventDetailsViewModel>(@event);
            return View(model);
        }

        // GET: Admin/Events/Create
        [Authorize(Roles = "Admin")]
        [Route("create")]
        public ActionResult Create()
        {
            //ViewBag.Id = new SelectList(db.Logos, "Id", "Caption");
            return View();
        }

        // POST: Admin/Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("create")]
        public ActionResult Create(EventBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                var @event = Mapper.Map<EventBindingModel, Event>(bind);
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(db.Logos, "Id", "Caption", @event.Id);
            return View(bind);
        }

        // GET: Admin/Events/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Event, EventBindingModel>(@event);
            //ViewBag.Id = new SelectList(db.Logos, "Id", "Caption", @event.Id);
            return View(model);
        }

        // POST: Admin/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Name, Location, PrizePool, Description, TierType, StartDate, EndDate, Logo")] EventBindingModel model)
        {
            if (ModelState.IsValid)
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
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Id = new SelectList(db.Logos, "Id", "Caption", @event.Id);
            return View(model);
        }

        // GET: Admin/Events/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<Event, EventViewModel>(@event);
            return View(model);
        }

        // POST: Admin/Events/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("associateadmin/{id}")]
        public ActionResult AssociateAdmin(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            //var model = Mapper.Map<Event, EventAdminBindingModel>(@event);
            var associatedAdmins = @event.EventAdmins;
            var associatedAdminsModel = 
                Mapper.Map<IEnumerable<RegisteredUser>, IEnumerable<EventAdminBindingModel>>(associatedAdmins);
            var availableAdmins = this.db.Users.ToList().Where(u => u.IsInGivenRole("EventAdmin")).Except(associatedAdmins);
            if (!availableAdmins.Any())
            {
                return this.RedirectToAction("Index", "Events");
            }
            var availableAdminsModel = Mapper.Map<IEnumerable<RegisteredUser>, IEnumerable<EventAdminBindingModel>>(availableAdmins);
            var model = new AssociateEventAdminBindingModel
                            {
                                Name = @event.Name,
                                AvailableAdmins = availableAdminsModel,
                                AssociatedAdmins = associatedAdminsModel
                            };
            

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("associateadmin/{id}")]
        public ActionResult AssociateAdmin(int id, [Bind(Include = "AssociatedAdmins, AvailableAdmins, AssociateEventAdminId")] AssociateEventAdminBindingModel bind)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            if (ModelState.IsValid)
            {
                var @event = db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                if (bind.AssociateEventAdminId != null)
                {
                    var eventUser = this.db.Users.Find(bind.AssociateEventAdminId);
                    @event.EventAdmins.Add(eventUser);
                    this.db.SaveChanges();
                    return this.RedirectToAction("AssociateAdmin", "Events");
                }
            }

            return this.View(bind);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("dissociateadmin/{id}")]
        public ActionResult DissociateAdmin(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            //var model = Mapper.Map<Event, EventAdminBindingModel>(@event);
            var associatedAdmins = @event.EventAdmins;
            if (!associatedAdmins.Any())
            {
                return this.RedirectToAction("Index", "Events");
            }

            var associatedAdminsModel =
                Mapper.Map<IEnumerable<RegisteredUser>, IEnumerable<EventAdminBindingModel>>(associatedAdmins).ToList();
            //var availableAdmins = this.db.Users.ToList().Where(u => u.IsInGivenRole("EventAdmin")).Except(associatedAdmins);
            //var availableAdminsModel = Mapper.Map<IEnumerable<RegisteredUser>, IEnumerable<EventAdminBindingModel>>(availableAdmins);
            var model = new AssociateEventAdminBindingModel
            {
                Name = @event.Name,
                AvailableAdmins = associatedAdminsModel,
                AssociatedAdmins = associatedAdminsModel
            };


            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("dissociateadmin/{id}")]
        public ActionResult DissociateAdmin(int id, [Bind(Include = "AssociatedAdmins, AvailableAdmins, AssociateEventAdminId")] AssociateEventAdminBindingModel bind)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var @event = db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                if (bind.AssociateEventAdminId != null)
                {
                    var eventUser = this.db.Users.Find(bind.AssociateEventAdminId);
                    @event.EventAdmins.Remove(eventUser);
                    this.db.SaveChanges();
                    return this.RedirectToAction("DissociateAdmin", "Events");
                }
            }

            return this.View(bind);
        }
    }
}
