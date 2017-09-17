namespace ESportsEventsApp.Areas.LocationAdmin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;


    using ViewModels;

    using global::Models;
    [Authorize(Roles = "LocationAdmin")]
    [RouteArea("LocationAdmin", AreaPrefix = "locationadmin")]
    [RoutePrefix("venues")]
    public class VenuesController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: LocationAdmin/Venues
        [HttpGet]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index(string sortValue, string sortOrder)
        {
            var venues = this.db.Venues.Where(v=>v.Name != "No venue").ToList();
            var model = Mapper.Map<IEnumerable<Venue>, IEnumerable<VenueViewModel>>(venues);
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

                default:
                    {
                        throw new InvalidOperationException("Invalid sort parameters");
                    }
            }
            return View(model);
        }

        // GET: LocationAdmin/Venues/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Venue, VenueViewModel>(venue);

            return View(model);
        }

        // GET: LocationAdmin/Venues/Create
        [Route("create")]
        public ActionResult Create()
        {
            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            var availableCities = this.db.Cities.ToList();
            var availableCitiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
            var model = new VenueBindingModel
            {
                AvailableCountries = availableCountriesModel,
                AvailableCities = availableCitiesModel
            };

            return View(model);
        }

        // POST: LocationAdmin/Venues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(VenueBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                var venue = Mapper.Map<VenueBindingModel, Venue>(bind);
                var country = this.db.Countries.Find(bind.CountryId);
                venue.Country = country;
                var city = this.db.Cities.Find(bind.CityId);
                venue.City = city;
                db.Venues.Add(venue);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bind);
        }

        // GET: LocationAdmin/Venues/Edit/5
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Venue, VenueBindingModel>(venue);
            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            model.AvailableCountries = availableCountriesModel;

            var availableCities = this.db.Cities.ToList();
            var availableCitiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
            model.AvailableCities = availableCitiesModel;

            return View(model);
        }

        // POST: LocationAdmin/Venues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(VenueBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                var venue = this.db.Venues.Find(bind.Id);
                venue = Mapper.Map<VenueBindingModel, Venue>(bind);
                this.db.Venues.AddOrUpdate(a => a.Id, venue);
                db.SaveChanges();

                venue = this.db.Venues.Find(bind.Id);
                var country = this.db.Countries.Find(bind.CountryId);
                venue.Country = country;
                this.db.Entry(venue.Country).State = EntityState.Modified;

                var city = this.db.Cities.Find(bind.CityId);
                venue.City = city;
                this.db.Entry(venue.City).State = EntityState.Modified;
                this.db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bind);
        }

        // GET: LocationAdmin/Venues/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Venue, VenueBindingModel>(venue);

            return View(model);
        }

        // POST: LocationAdmin/Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var venue = db.Venues.Find(id);
            db.Venues.Remove(venue);
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
    }
}
