using System.Web.Mvc;

namespace ESportsEventsApp.Areas.PlayerAdmin
{
    public class PlayerAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PlayerAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PlayerAdmin_default",
                "PlayerAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}