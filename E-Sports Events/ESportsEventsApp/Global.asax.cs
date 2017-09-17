using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ESportsEventsApp
{
    using AutoMapper;

    using BindingModels;

    using Extensions;

    using global::Models;
    using global::Models.Images;
    using global::Models.Videos;

    using Microsoft.AspNet.Identity.EntityFramework;

    using ViewModels;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterMaps();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMaps()
        {
            Mapper.Initialize(
                expression =>
                    {
                        expression.CreateMap<IdentityUserRole, RoleViewModel>();
                        expression.CreateMap<RegisteredUser, RegisteredUserViewModel>()
                        .ForMember(dest=>dest.Roles, src=>src.MapFrom(s=>s.GetRoles()));
                        expression.CreateMap<Event, BasicEventViewModel>();
                        expression.CreateMap<Article, BasicArticleViewModel>();
                        expression.CreateMap<EditUserBindingModel, RegisteredUser>();
                        expression.CreateMap<RegisteredUser, EditUserBindingModel>();
                        expression.CreateMap<RegisteredUser, RegisteredUserDetailsViewModel>()
                        .ForMember(dest => dest.Roles, src => src.MapFrom(s => s.GetRoles()));
                        expression.CreateMap<RegisterBindingModel, RegisteredUser>();
                        expression.CreateMap<Event, EventViewModel>();
                        expression.CreateMap<Event, EventDetailsViewModel>();
                        expression.CreateMap<Event, EventDetailsBindingModel>();
                        expression.CreateMap<Logo, LogoBindingModel>();
                        expression.CreateMap<Event, EventBindingModel>();
                        expression.CreateMap<LogoBindingModel, Logo>();
                        expression.CreateMap<EventBindingModel, Event>();
                        expression.CreateMap<Logo, LogoViewModel>();
                        expression.CreateMap<RegisteredUser, EventAdminViewModel>();
                        expression.CreateMap<RegisteredUser, EventAdminBindingModel>();
                        expression.CreateMap<EventImage, EventImageViewModel>();
                        expression.CreateMap<EventImageBindingModel, EventImage>();
                        expression.CreateMap<Event, EventAllDetailsViewModel>();
                        expression.CreateMap<EventImage, RemEventImageBindingModel>();
                        expression.CreateMap<RemEventImageBindingModel, EventImage>();
                        expression.CreateMap<Event, RemoveEventImageBindingModel>()
                        .ForMember(dest=>dest.AddedEventImages, src=>src.MapFrom(e=>e.EventImages));
                        expression.CreateMap<EventVideo, EventVideoBindingModel>();
                        expression.CreateMap<EventVideoBindingModel, EventVideo>();
                        expression.CreateMap<EventVideo, EventVideoViewModel>();
                        expression.CreateMap<EventVideo, RemEventVideoBindingModel>();
                        expression.CreateMap<RemEventVideoBindingModel, EventVideo>();
                        expression.CreateMap<Event, RemoveEventVideoBindingModel>()
                        .ForMember(dest => dest.AddedEventVideos, src => src.MapFrom(e => e.EventVideos));
                        expression.CreateMap<Player, PlayerViewModel>();
                        expression.CreateMap<Player, PlayerBindingModel>();
                        expression.CreateMap<PlayerBindingModel, Player>();
                        expression.CreateMap<Team, TeamViewModel>();
                        expression.CreateMap<TeamBindingModel, Team>();
                        expression.CreateMap<Team, TeamBindingModel>();
                        expression.CreateMap<Season, SeasonViewModel>();
                        expression.CreateMap<CountryBindingModel, Country>();
                        expression.CreateMap<Country, CountryBindingModel>()
                        .ForMember(dest=>dest.Flag, t=>t.Ignore());
                        expression.CreateMap<Country, CountryViewModel>();
                        expression.CreateMap<PlayerImage, PlayerImageViewModel>();
                        expression.CreateMap<TeamLogo, TeamLogoViewModel>();
                        expression.CreateMap<Flag, FlagViewModel>();
                        expression.CreateMap<FlagBindingModel, Flag>();
                        expression.CreateMap<TeamLogoBindingModel, TeamLogo>();
                        expression.CreateMap<CityBindingModel, City>();
                        expression.CreateMap<City, CityBindingModel>();
                        expression.CreateMap<City, CityViewModel>();
                        expression.CreateMap<Country, CountryShortViewModel>();
                        expression.CreateMap<City, CityShortViewModel>();
                        expression.CreateMap<Team, TeamShortViewModel>();
                        expression.CreateMap<RoundBindingModel, Round>();
                        expression.CreateMap<Round, RoundBindingModel>();
                        expression.CreateMap<Round, RoundViewModel>();
                        expression.CreateMap<Venue, VenueViewModel>();
                        expression.CreateMap<Venue, VenueBindingModel>();
                        expression.CreateMap<VenueBindingModel, Venue>();
                        expression.CreateMap<Venue, VenueShortViewModel>();
                        expression.CreateMap<Flag, FlagBindingModel>();
                        expression.CreateMap<TeamLogo, TeamLogoBindingModel>();
                        expression.CreateMap<PlayerImage, PlayerImageBindingModel>()
                        .ForMember(dest=>dest.Alias,src=>src.MapFrom(s=>s.Player.Alias));
                        expression.CreateMap<PlayerImageBindingModel, PlayerImage>();
                    });
        }
    }
}
