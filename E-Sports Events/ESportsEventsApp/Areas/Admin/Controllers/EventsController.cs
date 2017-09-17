namespace ESportsEventsApp.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using Extensions;

    using global::Models;
    using global::Models.Images;

    using Helpers.Enums;

    using ViewModels;

    using Constants = Helpers.Constants;

    [Authorize(Roles = "Admin")]
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("events")]
    public class EventsController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: Admin/Events
        [HttpGet]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index(string sortValue, string sortOrder)
        {
            var events = db.Events.ToList();
            var model = Mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            this.ViewBag.SortValue = sortValue;
            this.ViewBag.SortOrder = sortOrder;
            switch (sortValue)
            {
                case null:
                    {
                        model = model.OrderBy(m => m.Name);
                        this.ViewBag.SortValue = "Name";
                        this.ViewBag.SortOrder = "Asc";
                    }
                    break;

                case "Name":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Name) : model.OrderByDescending(m => m.Name);
                    }
                    break;

                case "Location":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Location) : model.OrderByDescending(m => m.Location);
                    }
                    break;

                case "Venue":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Venue.Name) : model.OrderByDescending(m => m.Venue.Name);
                    }
                    break;

                case "TierType":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.TierType) : model.OrderByDescending(m => m.TierType);
                    }
                    break;

                case "PrizePool":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.PrizePool) : model.OrderByDescending(m => m.PrizePool);
                    }
                    break;

                case "Season":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Season.Year) : model.OrderByDescending(m => m.Season.Year);
                    }
                    break;

                default:
                    {
                        throw new InvalidOperationException("Invalid sort parameters");
                    }
            }
            return View(model);
        }

        [Route("allbytiertype/{tierType}")]
        public ActionResult AllByTierType(TierType tierType, string sortValue, string sortOrder)
        {
            var events = db.Events.Where(e=>e.TierType == tierType).ToList();
            var model = Mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            this.ViewBag.SortValue = sortValue;
            this.ViewBag.SortOrder = sortOrder;
            switch (sortValue)
            {
                case null:
                    {
                        model = model.OrderBy(m => m.Name);
                        this.ViewBag.SortValue = "Name";
                        this.ViewBag.SortOrder = "Asc";
                    }
                    break;

                case "Name":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Name) : model.OrderByDescending(m => m.Name);
                    }
                    break;

                case "Location":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Location) : model.OrderByDescending(m => m.Location);
                    }
                    break;

                case "Venue":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Venue.Name) : model.OrderByDescending(m => m.Venue.Name);
                    }
                    break;

                case "PrizePool":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.PrizePool) : model.OrderByDescending(m => m.PrizePool);
                    }
                    break;

                case "Season":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Season.Year) : model.OrderByDescending(m => m.Season.Year);
                    }
                    break;

                default:
                    {
                        throw new InvalidOperationException("Invalid sort parameters");
                    }
            }
            ViewBag.TierType = tierType.ToString();
            return View(model);
        }

        [Route("allbyseason/{seasonId}")]
        public ActionResult AllBySeason(int seasonId, string sortValue, string sortOrder)
        {
            var events = db.Events.Where(e => e.Season.Id == seasonId).ToList();
            var model = Mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            this.ViewBag.SortValue = sortValue;
            this.ViewBag.SortOrder = sortOrder;
            switch (sortValue)
            {
                case null:
                    {
                        model = model.OrderBy(m => m.Name);
                        this.ViewBag.SortValue = "Name";
                        this.ViewBag.SortOrder = "Asc";
                    }
                    break;

                case "Name":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Name) : model.OrderByDescending(m => m.Name);
                    }
                    break;

                case "Location":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Location) : model.OrderByDescending(m => m.Location);
                    }
                    break;

                case "Venue":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Venue.Name) : model.OrderByDescending(m => m.Venue.Name);
                    }
                    break;

                case "TierType":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.TierType) : model.OrderByDescending(m => m.TierType);
                    }
                    break;

                case "PrizePool":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.PrizePool) : model.OrderByDescending(m => m.PrizePool);
                    }
                    break;

                default:
                    {
                        throw new InvalidOperationException("Invalid sort parameters");
                    }
            }
            var season = this.db.Seasons.Find(seasonId);
            ViewBag.Season = season?.Year ?? 0;
            return View(model);
        }

        // GET: Admin/Events/Details/5
        [HttpGet]
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

            ViewBag.StartDate = @event.StartDate;
            return View(model);
        }

        // GET: Admin/Events/Create
        [Route("create")]
        public ActionResult Create()
        {
            var countries = this.db.Countries;
            var countryModels = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(countries);
            var cities = this.db.Cities.ToList();
            var citiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(cities);
            var venues = this.db.Venues;
            var venuesModel = Mapper.Map<IEnumerable<Venue>, IEnumerable<VenueBindingModel>>(venues);
            var bindingModel = new EventBindingModel
            {
                AvailableCountries = countryModels,
                AvailableCities = citiesModel,
                AvailableVenues = venuesModel
            };

            return View(bindingModel);
        }

        // POST: Admin/Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(EventBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                //bind.Logo.Url = Constants.LogosFolderPath + bind.Logo.Url;
                var @event = Mapper.Map<EventBindingModel, Event>(bind);
                var season = this.db.Seasons.FirstOrDefault(s => s.Year == @event.EndDate.Value.Year);
                if (season == null)
                {
                    season = new Season { Year = @event.EndDate.Value.Year };
                }
                @event.Season = season;
                var country = this.db.Countries.Find(bind.CountryId);
                @event.Country = country;
                var city = this.db.Cities.Find(bind.CityId);
                @event.City = city;
                var venue = this.db.Venues.Find(bind.VenueId);
                @event.Venue = venue;
                db.Events.Add(@event);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            bind.AvailableCountries = availableCountriesModel;
            var availableCities = this.db.Cities.ToList();
            var availableCitiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
            bind.AvailableCities = availableCitiesModel;
            var venues = this.db.Venues;
            var availableVenuesModel = Mapper.Map<IEnumerable<Venue>, IEnumerable<VenueBindingModel>>(venues);
            bind.AvailableVenues = availableVenuesModel;

            return View(bind);
        }

        // GET: Admin/Events/Edit/5
        [HttpGet]
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

            var dateToString = @event.StartDate.Value.ToString("g");
            @event.StartDate = DateTime.Parse(dateToString, CultureInfo.CurrentCulture);

            dateToString = @event.EndDate.Value.ToString("g");
            @event.EndDate = DateTime.Parse(dateToString, CultureInfo.CurrentCulture);
            var model = Mapper.Map<Event, EventBindingModel>(@event);
            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            model.AvailableCountries = availableCountriesModel;
            var availableCities = this.db.Cities.ToList();
            var availableCitiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
            model.AvailableCities = availableCitiesModel;
            var venues = this.db.Venues;
            var availableVenuesModel = Mapper.Map<IEnumerable<Venue>, IEnumerable<VenueBindingModel>>(venues);
            model.AvailableVenues = availableVenuesModel;
            //ViewBag.Id = new SelectList(db.Logos, "Id", "Caption", @event.Id);
            return View(model);
        }

        // POST: Admin/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        //public ActionResult Edit([Bind(Include = "Id, Name, Location, PrizePool, Description, TierType, StartDate, EndDate, Logo")] EventBindingModel model)
        public ActionResult Edit(EventBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var @event = this.db.Events.Find(model.Id);
                @event = Mapper.Map<EventBindingModel, Event>(model);              
                this.db.Events.AddOrUpdate(a => a.Id, @event);
                this.db.SaveChanges();

                @event = this.db.Events.Find(model.Id);
                var country = this.db.Countries.Find(model.CountryId);
                @event.Country = country;
                this.db.Entry(@event.Country).State = EntityState.Modified;
                var city = this.db.Cities.Find(model.CityId);
                @event.City = city;
                this.db.Entry(@event.City).State = EntityState.Modified;
                var venue = this.db.Venues.Find(model.VenueId);
                if (@event.Venue == null)
                {
                    @event.Venue = venue;
                    //this.db.Entry(@event.Venue).State = EntityState.Added;
                }
                else
                {
                    @event.Venue = venue;
                    this.db.Entry(@event.Venue).State = EntityState.Modified;
                }
                var season = this.db.Seasons.FirstOrDefault(s => s.Year == @event.EndDate.Value.Year)
                             ?? new Season { Year = @event.EndDate.Value.Year };
                if ((@event.Season != null && @event.EndDate.Value.Year != season.Year) || @event.Season == null)
                {
                    @event.Season = season;
                }

                this.db.SaveChanges();

                return RedirectToAction("Index");
            }
            {
                if (model.Id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var @event = db.Events.Find(model.Id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                var dateToString = @event.StartDate.Value.ToString("g");
                @event.StartDate = DateTime.Parse(dateToString, CultureInfo.CurrentCulture);

                dateToString = @event.EndDate.Value.ToString("g");
                @event.EndDate = DateTime.Parse(dateToString, CultureInfo.CurrentCulture);
                model = Mapper.Map<Event, EventBindingModel>(@event);
                var availableCountries = this.db.Countries.ToList();
                var availableCountriesModel =
                    Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
                model.AvailableCountries = availableCountriesModel;
                var availableCities = this.db.Cities.ToList();
                var availableCitiesModel =
                    Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
                model.AvailableCities = availableCitiesModel;
                var venues = this.db.Venues;
                var availableVenuesModel = Mapper.Map<IEnumerable<Venue>, IEnumerable<VenueBindingModel>>(venues);
                model.AvailableVenues = availableVenuesModel;

                return View(model);

            }
        }

        // GET: Admin/Events/Delete/5
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

            var model = Mapper.Map<Event, EventBindingModel>(@event);
            return View(model);
        }

        // POST: Admin/Events/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(EventBindingModel bind)
        {
            var @event = db.Events.Find(bind.Id);
            db.Events.Remove(@event);

            var eventImages = @event.EventImages;
            this.db.EventImages.RemoveRange(eventImages);
            //var logo = @event.Logo;
            //this.db.EventImages.RemoveRange(eventImages);
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
        [Route("associateadmin/{id}")]
        public ActionResult AssociateAdmin(int? id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = this.db.Events.Find(id);
            if (@event == null)
            {
                return this.HttpNotFound();
            }
            if (@event.StartDate < DateTime.Now)
            {
                return this.RedirectToAction("Index");
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
                                AssociatedAdmins = associatedAdminsModel,
                                Id = (int)id
                            };

            this.ViewBag.StartDate = @event.StartDate;

            return this.View(model);
        }

        [HttpPost]
        [Route("associateadmin/{id}")]
        public ActionResult AssociateAdmin(int? id, [Bind(Include = "Id,AssociatedAdmins, AvailableAdmins, AssociateEventAdminId")] AssociateEventAdminBindingModel bind)
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
        [Route("dissociateadmin/{id}")]
        public ActionResult DissociateAdmin(int? id)
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
            if (@event.StartDate < DateTime.Now)
            {
                return this.RedirectToAction("Index");
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
                AssociatedAdmins = associatedAdminsModel,
                Id = (int)id
            };

            this.ViewBag.StartDate = @event.StartDate;
            return this.View(model);
        }

        [HttpPost]
        [Route("dissociateadmin/{id}")]
        public ActionResult DissociateAdmin(int? id, [Bind(Include = "Id, AssociatedAdmins, AvailableAdmins, AssociateEventAdminId")] AssociateEventAdminBindingModel bind)
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
                    if (eventUser != null)
                    {
                        @event.EventAdmins.Remove(eventUser);
                        this.db.SaveChanges();
                    }
                                      
                    return this.RedirectToAction("DissociateAdmin", "Events");
                }
            }

            return this.View(bind);
        }

        [HttpGet]
        [Route("editlogo/{id}")]
        public ActionResult EditLogo(int? id)
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
            if (@event.StartDate <= DateTime.Now)
            {
                return this.RedirectToAction("Index");
            }

            var model = Mapper.Map<Logo, LogoBindingModel>(@event.Logo) ?? new LogoBindingModel() { Id = (int)id };
            this.ViewBag.EventName = @event.Name;
            this.ViewBag.Id = id;
            this.ViewBag.StartDate = @event.StartDate;
            return this.View(model);
        }

        [HttpPost]
        [Route("editlogo/{id}")]
        public ActionResult EditLogo(int? id, LogoBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (@event.Logo != null && hasUrl)
                {
                    this.db.Entry(@event.Logo).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                if (hasUrl)
                {
                    model.Url = Constants.LogosFolderPath + model.Url;
                    var logo = Mapper.Map<LogoBindingModel, Logo>(model);
                    @event.Logo = logo;

                    this.db.SaveChanges();
                }

                return this.RedirectToAction("Index");

            }
            return this.View(model);
        }

        [HttpGet]
        [Route("editlogolocal/{id}")]
        public ActionResult EditLogoLocal(int? id)
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

            if (@event.StartDate <= DateTime.Now)
            {
                return this.RedirectToAction("Index");
            }

            var model = Mapper.Map<Logo, LogoBindingModel>(@event.Logo) ?? new LogoBindingModel() { Id = (int)id };
            this.ViewBag.EventName = @event.Name;
            this.ViewBag.StartDate = @event.StartDate;
            this.ViewBag.Id = id;
            return this.View(model);
        }

        [HttpPost]
        [Route("editlogolocal/{id}")]
        public ActionResult EditLogoLocal(int? id, LogoBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (@event.Logo != null && hasUrl)
                {
                    this.db.Entry(@event.Logo).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                var photo = Request.Files["Url"];
                if (photo == null)
                {
                    return this.View();
                }

                if (hasUrl)
                {
                    var directory = $"{Server.MapPath("~")}{Constants.LogosMapPath}";
                    photo.SaveAs(Path.Combine(directory, photo.FileName));
                    model.Url = Constants.LogosFolderPath + photo.FileName;
                    var logo = Mapper.Map<LogoBindingModel, Logo>(model);
                    @event.Logo = logo;
                    this.db.SaveChanges();
                }

                ViewBag.EventName = @event.Name;

                return this.RedirectToAction("Index");
            }
            return this.View();
        }
    }
}

