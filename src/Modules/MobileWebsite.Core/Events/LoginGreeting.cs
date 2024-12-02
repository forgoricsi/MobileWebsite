using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Users.Events;
using OrchardCore.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWebsite.Core.Events
{
    public class LoginGreeting : ILoginFormEvent
    {
        private readonly INotifier _notifier;
        private readonly IHtmlLocalizer T;

        public LoginGreeting(INotifier notifier, IHtmlLocalizer<LoginGreeting> htmlLocalizer)
        {
            _notifier = notifier;
            T = htmlLocalizer;
        }

        public Task IsLockedOutAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task LoggedInAsync(string userName)
        {
            _notifier.AddAsync(NotifyType.Success, T["Hi {0}!", userName]);
            return Task.CompletedTask;
        }

        public Task LoggedInAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task LoggingInAsync(string userName, Action<string, string> reportError) => Task.CompletedTask;

        public Task LogginginFailedAsync(string userName) => Task.CompletedTask;

        public Task LoggingInFailedAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task LoggingInFailedAsync(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
