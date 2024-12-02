using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWebsite.Core.Services
{
    [BackgroundTask(Schedule = "*/1 * * * *", Description = "Just a demo background task.")]
    public class DemoBackgroundTask : IBackgroundTask
    {
        public Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var contentManager = serviceProvider.GetRequiredService<IContentManager>();
            return Task.CompletedTask;
        }
    }
}
