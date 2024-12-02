using MobileWebsite.Core.Drivers;
using MobileWebsite.Core.Migrations;
using MobileWebsite.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using System;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.ContentManagement.Display.Models;
using MobileWebsite.Core.ViewModels;
using OrchardCore.DisplayManagement.ModelBinding;


namespace MobileWebsite.Core.Drivers
{
    public class PersonPartDisplayDriver : ContentPartDisplayDriver<PersonPart>
    {
        public override IDisplayResult Display(PersonPart part, BuildPartDisplayContext context) => 
            Initialize<PersonPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Detail", "Content:5")
            .Location("Summary", "Content:5");

        public override IDisplayResult Edit(PersonPart part, BuildPartEditorContext context) =>
            Initialize<PersonPartViewModel>(
                GetEditorShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Content:5");

        /*public override async Task<IDisplayResult> UpdateAsync(PersonPart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new PersonPartViewModel();
            await updater.TryUpdateModelAsync(viewModel, Prefix);

            part.Name = viewModel.Name;
            part.Handedness = viewModel.Handedness;
            part.BirthDateUtc = viewModel.BirthDateUtc;

            return await EditAsync(part, context);
        }*/

        private static void PopulateViewModel(PersonPart part, PersonPartViewModel viewModel)
        {
            viewModel.Name = part.Name;
            viewModel.Handedness = part.Handedness;
            viewModel.BirthDateUtc = part.BirthDateUtc;
        }
    }
}
