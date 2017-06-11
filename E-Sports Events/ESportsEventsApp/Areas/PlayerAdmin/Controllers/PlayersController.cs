namespace ESportsEventsApp.Areas.PlayerAdmin.Controllers
{
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

    [Authorize(Roles = "PlayerAdmin")]
    [RouteArea("PlayerAdmin", AreaPrefix = "playeradmin")]
    [RoutePrefix("players")]
    public class PlayersController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();

        // GET: PlayerAdmin/Players
        [HttpGet]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index()
        {
            var players = db.Players.ToList();
            var model = Mapper.Map<IEnumerable<Player>, IEnumerable<PlayerViewModel>>(players);

            return View(model);
        }

        // GET: PlayerAdmin/Players/Details/5
        [HttpGet]
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var  player = db.Players.Find(id);
            if (player == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<Player, PlayerViewModel>(player);
            return View(model);
        }

        // GET: PlayerAdmin/Players/Create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var countries = this.db.Countries;
            var teams = this.db.Teams;
            var countryModels = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(countries);
            var teamModels = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamBindingModel>>(teams);
            var cities = this.db.Cities.ToList();
            var citiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(cities);
            var bindingModel = new PlayerBindingModel
                                   {
                                       AvailableCountries = countryModels,
                                       AvailableTeams = teamModels,
                                       AvailableCities = citiesModel
                                   };
            return View(bindingModel);
        }

        // POST: PlayerAdmin/Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(PlayerBindingModel bind)
        {
            if (this.ModelState.IsValid)
            {
                var player = Mapper.Map<PlayerBindingModel, Player>(bind);
                var country = this.db.Countries.Find(bind.CountryId);
                player.Country = country;
                var city = this.db.Cities.Find(bind.CityId);
                player.City = city;
                var team = this.db.Teams.Find(bind.TeamId);
                player.Team = team;
                this.db.Players.Add(player);
                this.db.SaveChanges();

                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(db.PlayerImages, "Id", "Caption", player.Id);
            return View(bind);
        }

        // GET: PlayerAdmin/Players/Edit/5
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player = db.Players.Find(id);
            if (player == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<Player, PlayerBindingModel>(player);
            var availableCountries = this.db.Countries.ToList();
            var availableCountriesModel =
                Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(availableCountries);
            model.AvailableCountries = availableCountriesModel;
            var availableCities = this.db.Cities.ToList();
            var availableCitiesModel =
                Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(availableCities);
            model.AvailableCities = availableCitiesModel;
            var availableTeams = this.db.Teams.ToList();
            var availableTeamsModel =
                Mapper.Map<IEnumerable<Team>, IEnumerable<TeamBindingModel>>(availableTeams);
            model.AvailableTeams = availableTeamsModel;

            return View(model);
        }

        // POST: PlayerAdmin/Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(PlayerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var player = this.db.Players.Find(model.Id);
                player = Mapper.Map<PlayerBindingModel, Player>(model);
                this.db.Players.AddOrUpdate(a => a.Id, player);
                this.db.SaveChanges();

                player = this.db.Players.Find(model.Id);

                var country = this.db.Countries.Find(model.CountryId);
                player.Country = country;
                this.db.Entry(player.Country).State = EntityState.Modified;

                var city = this.db.Cities.Find(model.CityId);
                player.City = city;
                this.db.Entry(player.City).State = EntityState.Modified;

                var team = this.db.Teams.Find(model.TeamId);
                player.Team = team;
                this.db.Entry(player.Team).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: PlayerAdmin/Players/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: PlayerAdmin/Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
