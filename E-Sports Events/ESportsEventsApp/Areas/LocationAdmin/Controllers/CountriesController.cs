namespace ESportsEventsApp.Areas.PlayerAdmin.Controllers
{
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
        public ActionResult Index()
        {
            var countries = db.Countries.ToList();
            var countriesModel = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryViewModel>>(countries);
            return View(countriesModel);
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
            var country = db.Countries.Find(id);
            if (country == null)
            {
                return this.HttpNotFound();
            }
            var model = Mapper.Map<Country, CountryViewModel>(country);
            return View(model);
        }

        // GET: PlayerAdmin/Countries/Create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlayerAdmin/Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(CountryBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                var country = Mapper.Map<CountryBindingModel, Country>(bind);
                db.Countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bind);
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
            var country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Country, CountryBindingModel>(country);

            return View(model);
        }

        // POST: PlayerAdmin/Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Name")] CountryBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var country = this.db.Countries.Find(model.Id);
                country = Mapper.Map<CountryBindingModel, Country>(model);
                this.db.Countries.AddOrUpdate(a => a.Id, country);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: PlayerAdmin/Countries/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var country = db.Countries.Find(id);

            if (country == null)
            {
                return HttpNotFound();
            }

            return View(country);
        }

        // POST: PlayerAdmin/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();
            return RedirectToAction("Index");
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
           
            ViewBag.CountryName = country.Name;
            return this.View();
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

                ViewBag.CountryName = country.Name;
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
           
            ViewBag.CountryName = country.Name;
            return this.View();
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

                var photo = Request.Files["Url"];
                if (photo == null)
                {
                    return this.View();
                }

                if (hasUrl)
                {
                    var directory = $"{Server.MapPath("~")}{Constants.FlagsMapPath}";
                    photo.SaveAs(Path.Combine(directory, photo.FileName));
                    model.Url = Constants.FlagsFolderPath + photo.FileName;
                    var flag = Mapper.Map<FlagBindingModel, Flag>(model);
                    country.Flag = flag;
                    this.db.SaveChanges();
                }

                ViewBag.CountryName = country.Name;

                return this.RedirectToAction("Index");
            }
            return this.View();
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
