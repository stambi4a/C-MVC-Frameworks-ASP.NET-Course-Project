namespace ESportsEventsApp.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using BindingModels;

    using Data;

    using global::Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;

    using ViewModels;

    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private ESportsEventsContext db = new ESportsEventsContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        // GET: Admin/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all")]
        [Route("index")]
        [Route("")]
        public ActionResult Index()
        {
            var users = this.db.Users.Include(u=>u.Roles).ToList();
            var model = Mapper.Map<IEnumerable<RegisteredUser>, IEnumerable<RegisteredUserViewModel>>(users);
            return View(model);
        }

        // GET: Admin/Users/Details/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("details")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var registeredUser = db.Users.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<RegisteredUser, RegisteredUserDetailsViewModel>(registeredUser);
            return View(model);
        }

        // GET: Admin/Users/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "Id, Name, Email, PasswordHash, PhoneNumber, DateAdded, UserName")] RegisteredUserBindingModel bind
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("create")]
        public ActionResult Create(RegisterBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<RegisterBindingModel, RegisteredUser>(bind);
                user.DateAdded = DateTime.Now;
                var result = UserManager.Create(user, bind.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Users");
                }
            }

            return View(bind);
        }

        // GET: Admin/Users/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var registeredUser = db.Users.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<RegisteredUser, EditUserBindingModel>(registeredUser);
            return View(model);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Name, Email, PasswordHash, PhoneNumber, DateAdded, UserName")] EditUserBindingModel bind)
        {
            if (ModelState.IsValid)
            {
                var registeredUser = Mapper.Map<EditUserBindingModel, RegisteredUser>(bind);
                db.Entry(registeredUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bind);
        }

        // GET: Admin/Users/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var registeredUser = db.Users.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<RegisteredUser, RegisteredUserViewModel>(registeredUser);
            return View(model);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var registeredUser = db.Users.Find(id);
            db.Users.Remove(registeredUser);
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
        [Route("associaterole/{id}")]
        public ActionResult AssociateRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            var context = new ESportsEventsContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var allRoles = roleManager.Roles.ToList();
            var AssociatedRoles = user.Roles.Select(r => allRoles.FirstOrDefault(ar => ar.Id == r.RoleId).Name);
            var model = new AssociateRoleBindingModel()
            {
                AssociatedRoles = AssociatedRoles,
                Id = user.Id,
                Username = user.UserName
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("associaterole/{id}")]
        public ActionResult AssociateRole([Bind(Include = "Id, AddedRole, Username")] AssociateRoleBindingModel bind)
        {
            if (bind.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(bind.Id);
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (string.IsNullOrEmpty(bind.AddedRole))
                {
                    this.ModelState.AddModelError("role", "There are no more roles to be added");
                    var context = new ESportsEventsContext();
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    var allRoles = roleManager.Roles.ToList();
                    var AssociatedRoles = user.Roles.Select(r => allRoles.FirstOrDefault(ar => ar.Id == r.RoleId).Name);
                    bind.AssociatedRoles = AssociatedRoles;
                    //bind.Username = user.UserName;
                    return this.View(bind);
                }
                var result = userManager.AddToRole(user.Id, bind.AddedRole);
                //userManager.Update(user);
                if (result == IdentityResult.Failed())
                {
                    this.ModelState.AddModelError("role", "This user can not be assigned the given role.");
                    var context = new ESportsEventsContext();
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    var allRoles = roleManager.Roles.ToList();
                    var AssociatedRoles = user.Roles.Select(r => allRoles.FirstOrDefault(ar => ar.Id == r.RoleId).Name);
                    bind.AssociatedRoles = AssociatedRoles;
                    //bind.Username = user.UserName;
                    return this.View(bind);
                }

                return this.RedirectToAction("AssociateRole", "Users");
            }

            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("dissociaterole/{id}")]
        public ActionResult DissociateRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            var context = new ESportsEventsContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var allRoles = roleManager.Roles.ToList();
            var AssociatedRoles = user.Roles.Select(r => allRoles.FirstOrDefault(ar => ar.Id == r.RoleId).Name);
            var model = new DissociateRoleBindingModel
            {
                AssociatedRoles = AssociatedRoles,
                Id = id,
                Username = user.UserName
            };
            if (user == null)
            {
                return HttpNotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("dissociaterole/{id}")]
        public ActionResult DissociateRole([Bind(Include = "Id, RoleToRemove, Username")] DissociateRoleBindingModel bind)
        {
            if (bind.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(bind.Id);
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (string.IsNullOrEmpty(bind.RoleToRemove))
                {
                    this.ModelState.AddModelError("role", "There are no more roles to be removed");
                    var context = new ESportsEventsContext();
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    var allRoles = roleManager.Roles.ToList();
                    var AssociatedRoles = user.Roles.Select(r => allRoles.FirstOrDefault(ar => ar.Id == r.RoleId).Name);
                    bind.AssociatedRoles = AssociatedRoles;
                    //bind.Username = user.UserName;
                    return this.View(bind);
                }
                var result = userManager.RemoveFromRole(user.Id, bind.RoleToRemove);
                //userManager.Update(user);
                if (result == IdentityResult.Failed())
                {
                    this.ModelState.AddModelError("role", "This user can not be dissociated this role");
                    var context = new ESportsEventsContext();
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    var allRoles = roleManager.Roles.ToList();
                    var AssociatedRoles = user.Roles.Select(r => allRoles.FirstOrDefault(ar => ar.Id == r.RoleId).Name);
                    bind.AssociatedRoles = AssociatedRoles;
                    //bind.Username = user.UserName;
                    return this.View(bind);
                }

                return this.RedirectToAction("DissociateRole", "Users");
            }

            return this.View();
        }
    }
}
