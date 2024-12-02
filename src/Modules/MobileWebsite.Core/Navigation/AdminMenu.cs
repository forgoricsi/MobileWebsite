using Microsoft.Extensions.Localization;
using MobileWebsite.Core.Permissions;
using NetTopologySuite.Algorithm;
using OrchardCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWebsite.Core.Navigation
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer) => T = localizer;

        public ValueTask BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
                return ValueTask.CompletedTask;

            builder.Add(T["Person Page Admin"], "5", item => item
            .Action("CustomAuthorization", "Admin", new { Area = "DojoCourse.Module" })
            .Permission(PersonPagePermissions.ManagePersonPages));

            return ValueTask.CompletedTask;
        }
    }
}
