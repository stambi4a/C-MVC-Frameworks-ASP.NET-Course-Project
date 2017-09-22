﻿namespace ESportsEventsApp.Areas.PlayerAdmin.Controllers
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
        public ActionResult Index(string sortValue, string sortOrder)
        {
            var players = db.Players.ToList();
            var model = Mapper.Map<IEnumerable<Player>, IEnumerable<PlayerViewModel>>(players);
            this.ViewBag.SortValue = sortValue;
            this.ViewBag.SortOrder = sortOrder;
            switch (sortValue)
            {
                case null:
                    {
                        model = model.OrderBy(m => m.Alias);
                        this.ViewBag.SortValue = "Alias";
                        this.ViewBag.SortOrder = "Asc";
                    }
                    break;

                case "Alias":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Alias) : model.OrderByDescending(m => m.Alias);
                    }
                    break;

                case "Team":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Team.Name) : model.OrderByDescending(m => m.Team.Name);
                    }
                    break;

                case "Location":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.Location) : model.OrderByDescending(m => m.Location);
                    }
                    break;

                case "PrizeMoney":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.PrizeMoney) : model.OrderByDescending(m => m.PrizeMoney);
                    }
                    break;

                case "BespaPoints":
                    {
                        model = sortOrder.Equals("Asc") ? model.OrderBy(m => m.BespaPoints) : model.OrderByDescending(m => m.BespaPoints);
                    }
                    break;

                default:
                    {
                        throw new InvalidOperationException("Invalid sort parameters");
                    }
            }
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
            var countries = this.db.Countries.ToList();
            var teams = this.db.Teams.ToList();
            var countryModels = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(countries);
            var teamModels = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamBindingModel>>(teams);
            var cities = this.db.Cities.ToList();
            var citiesModel = Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(cities);
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

                return this.RedirectToAction("Index");
            }

            var countries = this.db.Countries.ToList();
            var teams = this.db.Teams.ToList();
            var countryModels = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryBindingModel>>(countries);
            var teamModels = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamBindingModel>>(teams);
            var cities = this.db.Cities.ToList();
            var citiesModel = Mapper.Map<IEnumerable<City>, IEnumerable<CityBindingModel>>(cities);
            var bindingModel = new PlayerBindingModel
            {
                AvailableCountries = countryModels,
                AvailableTeams = teamModels,
                AvailableCities = citiesModel
            };

            return View(bindingModel);
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
            var player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Player, PlayerBindingModel>(player);

            return View(model);
        }

        // POST: PlayerAdmin/Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editplayerimage/{id}")]
        public ActionResult EditPlayerImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var player = this.db.Players.Find(id);
            if (player == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<PlayerImage, PlayerImageBindingModel>(player.PlayerImage) ?? new PlayerImageBindingModel() { Id = (int)id };
            this.ViewBag.Alias = player.Alias;
            return this.View(model);
        }

        [HttpPost]
        [Route("editplayerimage/{id}")]
        public ActionResult EditPlayerImage(int? id, PlayerImageBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var player = this.db.Players.Find(id);
                if (player == null)
                {
                    return this.HttpNotFound();
                }
                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (player.PlayerImage != null && hasUrl)
                {
                    this.db.Entry(player.PlayerImage).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                if (hasUrl)
                {
                    model.Url = Constants.PlayerImagesFolderPath + model.Url;
                    var playerImage = Mapper.Map<PlayerImageBindingModel, PlayerImage>(model);
                    player.PlayerImage = playerImage;

                    this.db.SaveChanges();
                }

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        [Route("editplayerimagelocal/{id}")]
        public ActionResult EditPlayerImageLocal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var player = this.db.Players.Find(id);
            if (player == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<PlayerImage, PlayerImageBindingModel>(player.PlayerImage) ?? new PlayerImageBindingModel() { Id = (int)id };
            this.ViewBag.Alias = player.Alias;
            return this.View(model);
        }

        [HttpPost]
        [Route("editplayerimagelocal/{id}")]
        public ActionResult EditPlayerImageLocal(int? id, PlayerImageBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var player = this.db.Players.Find(id);
                if (player == null)
                {
                    return this.HttpNotFound();
                }
                var hasUrl = !string.IsNullOrEmpty(model.Url);
                if (player.PlayerImage != null && hasUrl)
                {
                    this.db.Entry(player.PlayerImage).State = EntityState.Deleted;
                    this.db.SaveChanges();
                }

                var image = Request.Files["Url"];
                if (image == null)
                {
                    return this.View();
                }
                var photo = new WebImage(image.InputStream);
                if (model.X > 0)
                {
                    ImageHelper.CropCanvasImage(photo, model.X, model.Y, model.Width, model.Height);
                }

                ImageHelper.CropPlayerImage(photo);
                if (hasUrl)
                {
                    var directory = $"{Server.MapPath("~")}{Constants.PlayerImagesMapPath}";
                    photo.Save(Path.Combine(directory, image.FileName));

                    model.Url = Constants.PlayerImagesFolderPath + image.FileName;
                    var playerImage = Mapper.Map<PlayerImageBindingModel, PlayerImage>(model);
                    player.PlayerImage = playerImage;
                    this.db.SaveChanges();
                }

                this.ViewBag.Alias = player.Alias;

                return this.RedirectToAction("Index");
            }

            return this.View(model);
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
