using MobileWebsite.Core.Models;
using OrchardCore.ContentManagement.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWebsite.Core.Handlers
{
    public class PersonPartHandler : ContentPartHandler<PersonPart>
    {
        public override Task UpdatedAsync(UpdateContentContext context, PersonPart instance)
        {
            context.ContentItem.DisplayText = instance.Name;
            return Task.CompletedTask;
        }
    }
}
