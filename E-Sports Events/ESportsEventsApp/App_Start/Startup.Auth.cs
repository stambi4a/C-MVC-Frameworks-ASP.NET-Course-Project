using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Owin;

namespace ESportsEventsApp
{
    using System.Linq;

    using Data;

    using global::Models;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security.Google;

    using Owin.Security.Providers.BattleNet;

    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ESportsEventsContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, RegisteredUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
               appId: "1987190211511464",
               appSecret: "d2fd380a0a0e92f4956b2035f5cd0efb");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "803784889632-4uftia21btlie62op53u13kg1bs59kc2.apps.googleusercontent.com",
                ClientSecret = "fj9o51PW58xJauuzhKRlO_SS"
            });
            app.UseBattleNetAuthentication(new BattleNetAuthenticationOptions
            {
                ClientId = "ezfm5tcnjcdqj4vr9xfgubs9z4h3rm2v",
                ClientSecret = "ET9ep88BH8hkeeG3B7U86uQ2BQA8Vwcy"
            });
            //app.UseSteamAuthentication(applicationKey: "");
        }

        public void ConfigureRoles()
        {
            var context = new ESportsEventsContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<RegisteredUser>(new UserStore<RegisteredUser>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
                this.CreateAdministrator(userManager);
            }

            if (!roleManager.RoleExists("EventAdmin"))
            {
                var role = new IdentityRole("EventAdmin");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Buyer"))
            {
                var role = new IdentityRole("Buyer");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("ArticleAuthor"))
            {
                var role = new IdentityRole("ArticleAuthor");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Volunteer"))
            {
                var role = new IdentityRole("Volunteer");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Guest"))
            {
                var role = new IdentityRole("Guest");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("PlayerAdmin"))
            {
                var role = new IdentityRole("PlayerAdmin");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("LocationAdmin"))
            {
                var role = new IdentityRole("LocationAdmin");
                roleManager.Create(role);
            }
        }

        private void CreateAdministrator(UserManager<RegisteredUser> userManager)
        {
            var user = new RegisteredUser()
            {
                UserName = "stambi4a",
                Name = "Stanimir Todorov",
                Email = "stambi4a@softuni.bg",
                DateAdded = DateTime.Now
            };
            var pass = "Str18.";
            var createResult = userManager.Create(user, pass);
            if (createResult.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, "Admin");
            }
        }

        private void CreateUserInRole(string roleName, string username, UserManager<RegisteredUser> userManager)
        {
            var user = userManager.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}