using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Mvc;
using MobileWebsite.Core.Indexes;
using MobileWebsite.Core.Models;
using NodaTime;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql;

namespace MobileWebsite.Core.Controllers
{
    public class PersonController : Controller
    {
        private readonly ISession _session;
        private readonly IContentManager _contentManager;
        private readonly IClock _clock;
        public PersonController(ISession session, IContentManager contentManager, IClock clock)
        {
            _session = session;
            _contentManager = contentManager;
            _clock = clock;
        }

        [Route("PersonList")]
        public async Task<string> List()
        {
            var threshold = _clock.GetCurrentInstant().Minus(Duration.FromDays(365 * 40));
            var personPages = await _session.Query<ContentItem, ContentItemIndex>(index => index.ContentType == "PersonPage")
                .With<PersonPartIndex>(personPartIndex => personPartIndex.BirthDateUtc != null && personPartIndex.BirthDateUtc < threshold.ToDateTimeUtc())
                .ListAsync();

            foreach (var personPage in personPages)
            {
                await _contentManager.LoadAsync(personPage);
            }

            return string.Join(", ", personPages.Select(personPage => personPage.As<PersonPart>().Name));
        }
    }
}
