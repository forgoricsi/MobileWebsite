using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using MobileWebsite.Core.Indexes;
using MobileWebsite.Core.Models;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using YesSql.Sql;

namespace MobileWebsite.Core.Migrations
{
    class PersonMigration : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public PersonMigration(IContentDefinitionManager contentDefinitionManager) => _contentDefinitionManager = contentDefinitionManager;

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(PersonPart), part => part
            .Attachable()
            .WithField(nameof(PersonPart.Biography), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Biography")
                .WithSettings(new TextFieldSettings
                {
                    Hint = "Enter the biography of the person."
                })
                .WithEditor("TextArea"))
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("PersonPage", type => type
                .Creatable()
                .Listable()
                .WithPart(nameof(PersonPart))
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("PersonWidget", type => type
                .WithPart(nameof(PersonPart))
                .Stereotype("Widget")
            );

            return 2;
        }

        public int UpdateFrom1()
        {
            _contentDefinitionManager.AlterTypeDefinitionAsync("PersonWidget", type => type
                .WithPart(nameof(PersonPart))
                .Stereotype("Widget")
            );

            return 2;
        }

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(PersonPart), part => part
                .Attachable()
                .WithField(nameof(PersonPart.Biography), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Biography")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "Enter the biography of the person."
                    })
                    .WithEditor("TextArea"))
            );

            await _contentDefinitionManager.AlterTypeDefinitionAsync("PersonPage", type => type
                .Creatable()
                .Listable()
                .WithPart(nameof(PersonPart))
            );

            await _contentDefinitionManager.AlterTypeDefinitionAsync("PersonWidget", type => type
                .WithPart(nameof(PersonPart))
                .Stereotype("Widget")
            );

            return 2;
        }

        public async Task<int> UpdateFrom2Async()
        {
            await SchemaBuilder.CreateMapIndexTableAsync(typeof(PersonPartIndex), table => table
                .Column<string>(nameof(PersonPartIndex.ContentItemId), column => column.WithLength(26))
                .Column<DateTime>(nameof(PersonPartIndex.BirthDateUtc)), null);

            await SchemaBuilder.AlterTableAsync(nameof(PersonPartIndex), table => table
                .CreateIndex($"IDX_{nameof(PersonPartIndex)}_{nameof(PersonPartIndex.BirthDateUtc)}",
                nameof(PersonPartIndex.BirthDateUtc))
            );

            return 3;
        }
    }
}
