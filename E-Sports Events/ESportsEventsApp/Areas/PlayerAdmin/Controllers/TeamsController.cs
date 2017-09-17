namespace ESportsEventsApp.Areas.PlayerAdmin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Helpers;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using global::Models;
    using global::Models.Images;

    using Helpers;

    using Owin.Security.Providers.Imgur.Provider;

    using ViewModels;

    [Authorize(Roles = "PlayerAdmin")]
    [RouteArea("PlayerAdmin", AreaPrefix = "playeradmin")]
    [RoutePrefix("teams")]
    public class TeamsController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: PlayerAdmin/Teams
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index(string sortValue, string sortOrder)
        {
            var teams = db.Teams.Where(p => p.Name != "No team").ToList();
            var model = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamViewModel>>(teams);
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
            return this.View(model);
        }

        // GET: PlayerAdmin/Teams/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Team, TeamViewModel>(team);

            return View(model);
        }
        
        // GET: PlayerAdmin/Teams/Create
        [Route("create")]
        public ActionResult Create()
        {
            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            var availableCities = this.db.Cities.ToList();
            var availableCitiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
            var model = new TeamBindingModel
                            {
                                AvailableCountries = availableCountriesModel,
                                AvailableCities = availableCitiesModel
                            };

            return View(model);
        }

        // POST: PlayerAdmin/Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(TeamBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                var team = Mapper.Map<TeamBindingModel, Team>(bind);
                var country = this.db.Countries.Find(bind.CountryId);
                team.Country = country;
                var city = this.db.Cities.Find(bind.CityId);
                team.City = city;
                db.Teams.Add(team);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            var availableCities = this.db.Cities.ToList();
            var availableCitiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
            var model = new TeamBindingModel
            {
                AvailableCountries = availableCountriesModel,
                AvailableCities = availableCitiesModel
            };

            return View(model);
        }

        // GET: PlayerAdmin/Teams/Edit/5
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = db.Teams.Find(id);
            if (team == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<Team, TeamBindingModel>(team);
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

        // POST: PlayerAdmin/Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(TeamBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var team = this.db.Teams.Find(model.Id);
                team = Mapper.Map<TeamBindingModel, Team>(model);
                this.db.Teams.AddOrUpdate(a => a.Id, team);
                this.db.SaveChanges();

                var country = this.db.Countries.Find(model.CountryId);
                team = this.db.Teams.Find(model.Id);
                team.Country = country;
                this.db.Entry(team.Country).State = EntityState.Modified;

                var city = this.db.Cities.Find(model.CityId);
                team.City = city;
                this.db.Entry(team.City).State = EntityState.Modified;
                this.db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: PlayerAdmin/Teams/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Team, TeamBindingModel>(team);

            return View(model);
        }

        // POST: PlayerAdmin/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editteamlogo/{id}")]
        public ActionResult EditTeamLogo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = this.db.Teams.Find(id);
            if (team == null)
            {
                return this.HttpNotFound();
            }

            ViewBag.TeamName = team.Name;

            var model = Mapper.Map<TeamLogo, TeamLogoBindingModel>(team.TeamLogo)?? new TeamLogoBindingModel { Id = (int)id };
            return this.View(model);
        }

        [HttpPost]
        [Route("editteamlogo/{id}")]
        public ActionResult EditTeamLogo(int? id, TeamLogoBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var team = this.db.Teams.Find(id);
                if (team == null)
                {
                    return this.HttpNotFound();
                }
                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (team.TeamLogo != null && hasUrl)
                {
                    this.db.Entry(team.TeamLogo).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                if (hasUrl)
                {
                    model.Url = Constants.TeamLogosFolderPath + model.Url;
                    var teamLogo = Mapper.Map<TeamLogoBindingModel, TeamLogo>(model);
                    team.TeamLogo = teamLogo;

                    this.db.SaveChanges();
                }

                ViewBag.TeamName = team.Name;
                return this.RedirectToAction("Index");

            }
            return this.View(model);
        }

        [HttpGet]
        [Route("editteamlogolocal/{id}")]
        public ActionResult EditTeamLogoLocal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = this.db.Teams.Find(id);
            if (team == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.TeamName = team.Name;

            var model = Mapper.Map<TeamLogo, TeamLogoBindingModel>(team.TeamLogo) ?? new TeamLogoBindingModel { Id = (int)id };
            return this.View(model);
        }

        [HttpPost]
        [Route("editteamlogolocal/{id}")]
        public ActionResult EditTeamLogoLocal(int? id, TeamLogoBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var team = this.db.Teams.Find(id);
                if (team == null)
                {
                    return this.HttpNotFound();
                }

                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (team.TeamLogo != null && hasUrl)
                {
                    this.db.Entry(team.TeamLogo).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                var image = this.Request.Files["Url"];
                if (image == null)
                {
                    return this.View();
                }
                var photo = new WebImage(image.InputStream);
                if (photo.Width > 30)
                {
                    photo.Resize(30, 30, false);
                }

                if (hasUrl)
                {
                    var directory = $"{Server.MapPath("~")}{Constants.TeamLogosMapPath}";
                    photo.Save(Path.Combine(directory, photo.FileName));
                    model.Url = Constants.TeamLogosFolderPath + photo.FileName;
                    var teamLogo = Mapper.Map<TeamLogoBindingModel, TeamLogo>(model);
                    team.TeamLogo = teamLogo;
                    this.db.SaveChanges();
                }

                ViewBag.TeamName = team.Name;

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
