﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ESportsEventsApp
{
    using AutoMapper;

    using BindingModels;

    using ESportsEventsApp.Extensions;

    using global::Models;
    using global::Models.Images;

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
                        //.ForMember(dest => dest.Logo, src => src.Ignore());
                        expression.CreateMap<Event, EventDetailsViewModel>();
                        expression.CreateMap<Event, EventDetailsBindingModel>();
                        expression.CreateMap<Logo, LogoBindingModel>();
                        expression.CreateMap<Event, EventBindingModel>();
                        //.ForMember(dest=>dest.Logo, src=>src.Ignore());
                        expression.CreateMap<LogoBindingModel, Logo>();
                        expression.CreateMap<EventBindingModel, Event>();
                        //.ForMember(dest=>dest.Logo, src=>src.Ignore());
                        expression.CreateMap<Logo, LogoViewModel>();
                        expression.CreateMap<RegisteredUser, EventAdminViewModel>();
                        expression.CreateMap<RegisteredUser, EventAdminBindingModel>();
                    });
        }
    }
}
