namespace ESportsEventsApp.Areas.LocationAdmin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using global::Models;
    using global::Models.Images;

    using Helpers;

    using ViewModels;

    [Authorize(Roles = "LocationAdmin")]
    [RouteArea("LocationAdmin", AreaPrefix = "locationadmin")]
    [RoutePrefix("countries")]
    public class CountriesController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: PlayerAdmin/Countries
        [HttpGet]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index(string sortValue, string sortOrder)
        {
            var countries = this.db.Countries.Where(c => c.Name != "No country").ToList();
            var model = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryViewModel>>(countries);
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

                default:
                    {
                        throw new InvalidOperationException("Invalid sort parameters");
                    }
            }
            return this.View(model);
        }

        // GET: PlayerAdmin/Countries/Details/5
        [HttpGet]
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = this.db.Countries.Find(id);
            if (country == null)
            {
                return this.HttpNotFound();
            }
            var model = Mapper.Map<Country, CountryViewModel>(country);
            return this.View(model);
        }

        // GET: PlayerAdmin/Countries/Create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: PlayerAdmin/Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(CountryBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var country = Mapper.Map<CountryBindingModel, Country>(bind);
                this.db.Countries.Add(country);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(bind);
        }

        // GET: PlayerAdmin/Countries/Edit/5
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = this.db.Countries.Find(id);
            if (country == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<Country, CountryBindingModel>(country);

            return this.View(model);
        }

        // POST: PlayerAdmin/Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Name")] CountryBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var country = this.db.Countries.Find(model.Id);
                country = Mapper.Map<CountryBindingModel, Country>(model);
                this.db.Countries.AddOrUpdate(a => a.Id, country);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: PlayerAdmin/Countries/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var country = this.db.Countries.Find(id);

            if (country == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<Country, CountryBindingModel>(country);

            return this.View(model);
        }

        // POST: PlayerAdmin/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = this.db.Countries.Find(id);
            this.db.Countries.Remove(country);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editflag/{id}")]
        public ActionResult EditFlag(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = this.db.Countries.Find(id);
            if (country == null)
            {
                return this.HttpNotFound();
            }
           
            this.ViewBag.CountryName = country.Name;
            var model = Mapper.Map<Flag, FlagBindingModel>(country.Flag) ?? new FlagBindingModel { Id = (int)id };

            return this.View(model);
        }

        [HttpPost]
        [Route("editflag/{id}")]
        public ActionResult EditFlag(int? id, FlagBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var country = this.db.Countries.Find(id);
                if (country == null)
                {
                    return this.HttpNotFound();
                }
                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (country.Flag != null && hasUrl)
                {
                    this.db.Entry(country.Flag).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                if (hasUrl)
                {
                    model.Url = Constants.FlagsFolderPath + model.Url;
                    var flag = Mapper.Map<FlagBindingModel, Flag>(model);
                    country.Flag = flag;

                    this.db.SaveChanges();
                }

                this.ViewBag.CountryName = country.Name;
                return this.RedirectToAction("Index");

            }
            return this.View(model);
        }

        [HttpGet]
        [Route("editflaglocal/{id}")]
        public ActionResult EditFlagLocal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = this.db.Countries.Find(id);
            if (country == null)
            {
                return this.HttpNotFound();
            }
           
            this.ViewBag.CountryName = country.Name;
            var model = Mapper.Map<Flag, FlagBindingModel>(country.Flag) ?? new FlagBindingModel { Id = (int)id };

            return this.View(model);
        }

        [HttpPost]
        [Route("editflaglocal/{id}")]
        public ActionResult EditFlagLocal(int? id, FlagBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var country = this.db.Countries.Find(id);
                if (country == null)
                {
                    return this.HttpNotFound();
                }

                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (country.Flag != null && hasUrl)
                {
                    this.db.Entry(country.Flag).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                var photo = this.Request.Files["Url"];
                if (photo == null)
                {
                    return this.View();
                }

                if (hasUrl)
                {
                    var directory = $"{this.Server.MapPath("~")}{Constants.FlagsMapPath}";
                    photo.SaveAs(Path.Combine(directory, photo.FileName));
                    model.Url = Constants.FlagsFolderPath + photo.FileName;
                    var flag = Mapper.Map<FlagBindingModel, Flag>(model);
                    country.Flag = flag;
                    this.db.SaveChanges();
                }

                this.ViewBag.CountryName = country.Name;

                return this.RedirectToAction("Index");
            }
            return this.View();
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
