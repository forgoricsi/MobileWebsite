using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.ContentManagement;
using MobileWebsite.Core.Models;
using OrchardCore.Data.Migration;
using MobileWebsite.Core.Migrations;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using MobileWebsite.Core.Drivers;
using MobileWebsite.Core.Handlers;
using YesSql.Indexes;
using MobileWebsite.Core.Indexes;
using Microsoft.AspNetCore.Mvc;
using MobileWebsite.Core.Filters;
using OrchardCore.Users.Events;
using MobileWebsite.Core.Events;
using OrchardCore.Security.Permissions;
using MobileWebsite.Core.Permissions;
using OrchardCore.Navigation;
using MobileWebsite.Core.Navigation;
using MobileWebsite.Core.Services;
using OrchardCore.BackgroundTasks;

namespace MobileWebsite.Core
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<PersonPart>().UseDisplayDriver<PersonPartDisplayDriver>().AddHandler<PersonPartHandler>();
            services.AddScoped<IDataMigration, PersonMigration>();
            services.AddSingleton<IIndexProvider, PersonPartIndexProvider>();

            services.Configure<MvcOptions>(options => options.Filters.Add(typeof(ShapeInjectingFilter)));

            services.AddScoped<ILoginFormEvent, LoginGreeting>();

            services.AddScoped<IPermissionProvider, PersonPagePermissions>();

            services.AddScoped<INavigationProvider, OrchardCore.Admin.AdminMenu>();

            services.AddSingleton<IBackgroundTask, DemoBackgroundTask>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "MobileWebsite.Core",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
