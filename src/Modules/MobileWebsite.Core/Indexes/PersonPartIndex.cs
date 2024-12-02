using MobileWebsite.Core.Models;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YesSql.Indexes;

namespace MobileWebsite.Core.Indexes
{
    public class PersonPartIndex : MapIndex
    {
        public required string ContentItemId { get; set; }
        public DateTime? BirthDateUtc { get; set; }
    }

    public class PersonPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) => context.For<PersonPartIndex>()
            .Map(concentItem =>
            {
                try
                {
                    var personPart = concentItem.As<PersonPart>();

                    return personPart != null
                    ? new PersonPartIndex
                    {
                        ContentItemId = concentItem.ContentItemId,
                        BirthDateUtc = personPart.BirthDateUtc
                    }
                    : null;
                }
                catch (JsonException ex)
                {
                    // Log the exception or handle it as needed
                    Console.WriteLine($"Error deserializing PersonPart: {ex.Message}");
                    return null;
                }
            });
    }
}