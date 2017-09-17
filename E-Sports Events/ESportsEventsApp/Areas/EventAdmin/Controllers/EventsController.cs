namespace ESportsEventsApp.Areas.EventAdmin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using global::Models;
    using global::Models.Images;
    using global::Models.Videos;

    using Helpers.Enums;

    using Microsoft.AspNet.Identity;

    using ViewModels;

    using Constants = Helpers.Constants;

    [Authorize(Roles = "EventAdmin")]
    [RouteArea("EventAdmin", AreaPrefix = "eventadmin")]
    [RoutePrefix("myevents")]
    public class EventsController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: EventAdmin/Events
        [HttpGet]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index(string sortValue, string sortOrder)
        {
            var userId = this.GetCurrentUserId();
            var events = this.db.Events.ToList().Where(e=>e.EventAdmins.Any(ea=>ea.Id == userId));
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
            return this.View(model);
        }

        [Route("allbytiertype/{tierType}")]
        public ActionResult AllByTierType(TierType tierType, string sortValue, string sortOrder)
        {
            var userId = this.GetCurrentUserId();
            var events = db.Events.Where(e => e.EventAdmins.Any(ea => ea.Id == userId && e.TierType == tierType)).ToList();
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
            var userId = this.GetCurrentUserId();
            var events = db.Events.Where(e => e.EventAdmins.Any(ea => ea.Id == userId && e.Season.Id == seasonId)).ToList();
            var model = Mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            var season = this.db.Seasons.Find(seasonId);
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
            ViewBag.Season = season?.Year ?? 0;
            return View(model);
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

        // POST: EventAdmin/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
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
                @event.StartDate = DateTime.Parse(dateToString, CultureInfo.InvariantCulture);

                dateToString = @event.EndDate.Value.ToString("g");
                @event.EndDate = DateTime.Parse(dateToString, CultureInfo.InvariantCulture);
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
            //Add event images when event has already started - disabled during testing
            //if (@event.StartDate >= DateTime.Now)
            //{
            //    return this.RedirectToAction("Index");
            //}

            this.ViewBag.EventName = @event.Name;
            this.ViewBag.StartDate = @event.StartDate;
            this.ViewBag.Id = id;
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
            //Add event images when event has already started - disabled during testing
            //if (@event.StartDate >= DateTime.Now)
            //{
            //    return this.RedirectToAction("Index");
            //}

            this.ViewBag.EventName = @event.Name;
            this.ViewBag.StartDate = @event.StartDate;
            this.ViewBag.Id = id;
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

                var directory = $"{Server.MapPath("~")}{Constants.EventImagesMapPath}";
                photo.SaveAs(Path.Combine(directory, photo.FileName));
                bind.Url = Constants.EventImagesFolderPath + photo.FileName;
                if (@event.EventImages.All(ei => ei.Url != bind.Url))
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
            if (@event.StartDate < DateTime.Now)
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
            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                this.ViewBag.EventName = @event.Name;
                this.ViewBag.StartDate = @event.StartDate;
                return this.View();
            }
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

            if (@event.StartDate < DateTime.Now)
            {
                return this.RedirectToAction("Index");
            }

            var model = Mapper.Map<Logo, LogoBindingModel>(@event.Logo) ?? new LogoBindingModel() { Id = (int)id };
            this.ViewBag.EventName = @event.Name;
            this.ViewBag.StartDate = @event.StartDate;
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

            {
                var @event = this.db.Events.Find(id);
                if (@event == null)
                {
                    return this.HttpNotFound();
                }

                this.ViewBag.EventName = @event.Name;
                this.ViewBag.StartDate = @event.StartDate;
                return this.View();
            }           
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

            //Add event images when event has already started - disabled during testing
            //if (@event.StartDate >= DateTime.Now)
            //{
            //    return this.RedirectToAction("Index");
            //}

            var addedImages = @event.EventImages;
            if (addedImages.Count == 0)
            {
                return this.RedirectToAction("Index");
            }

            var model = Mapper.Map<Event, RemoveEventImageBindingModel>(@event);
            this.ViewBag.EventName = @event.Name;
            this.ViewBag.StartDate = @event.StartDate;
            this.ViewBag.Id = id;
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

            //Add event images when event has already started - disabled during testing
            //if (@event.StartDate >= DateTime.Now)
            //{
            //    return this.RedirectToAction("Index");
            //}

            this.ViewBag.EventName = @event.Name;
            this.ViewBag.StartDate = @event.StartDate;
            this.ViewBag.Id = id;
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
            this.ViewBag.EventName = @event.Name;
            this.ViewBag.StartDate = @event.StartDate;
            this.ViewBag.Id = id;
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
