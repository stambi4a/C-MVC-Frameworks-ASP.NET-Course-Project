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

    using global::Models;

    using ViewModels;

    [Authorize(Roles = "LocationAdmin")]
    [RouteArea("LocationAdmin", AreaPrefix = "locationadmin")]
    [RoutePrefix("cities")]
    public class CitiesController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: PlayerAdmin/Cities
        [HttpGet]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index(string sortValue, string sortOrder)
        {
            var cities = this.db.Cities.Where(c => c.Name != "No city").ToList();
            var model = Mapper.Map<IEnumerable<City>, IEnumerable<CityViewModel>>(cities);
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

                case "Country":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Country.Name) : model.OrderByDescending(m => m.Country.Name);
                    }
                    break;

                default:
                    {
                        throw new InvalidOperationException("Invalid sort parameters");
                    }
            }
            return this.View(model);
        }

        // GET: PlayerAdmin/Cities/Details/5
        [HttpGet]
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = this.db.Cities.Find(id);
            if (city == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<City, CityViewModel>(city);
            return this.View(model);
        }

        // GET: PlayerAdmin/Cities/Create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            var model = new CityBindingModel();
            model.AvailableCountries = availableCountriesModel;
            return this.View(model);
        }

        // POST: PlayerAdmin/Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(CityBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var city = Mapper.Map<CityBindingModel, City>(bind);
                var country = this.db.Countries.Find(bind.CountryId);
                city.Country = country;
                this.db.Cities.Add(city);
                this.db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(bind);
        }

        // GET: PlayerAdmin/Cities/Edit/5
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var city = this.db.Cities.Find(id);
            if (city == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<City, CityBindingModel>(city);
            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            model.AvailableCountries = availableCountriesModel;

            return this.View(model);
        }

        // POST: PlayerAdmin/Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(CityBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var city = this.db.Cities.Find(model.Id);
                city = Mapper.Map<CityBindingModel, City>(model);
                this.db.Cities.AddOrUpdate(a => a.Id, city);
                this.db.SaveChanges();

                var country = this.db.Countries.Find(model.CountryId);
                city = this.db.Cities.Find(model.Id);
                city.Country = country;
                this.db.Entry(city.Country).State = EntityState.Modified;
                this.db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: PlayerAdmin/Cities/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = this.db.Cities.Find(id);

            if (city == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<City, CityBindingModel>(city);

            return this.View(model);
        }

        // POST: PlayerAdmin/Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var city = this.db.Cities.Find(id);
            this.db.Cities.Remove(city);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
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
