using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.Contents;
using MobileWebsite.Core.Models;
using MobileWebsite.Core.Permissions;


namespace MobileWebsite.Core.Controllers
{
    public class AdminController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IAuthorizationService _authorizerService;
        public AdminController(IContentManager contentManager, IAuthorizationService authorizerService) 
        { 
            _contentManager = contentManager;
            _authorizerService = authorizerService;
        }

        public string Index()
        {
            return "Ok";
        }

        public async Task<string> ContentAuthorization()
        {
            var personPage = await _contentManager.NewAsync("PersonPage");

            var isAuthorized = await _authorizerService.AuthorizeAsync(User, CommonPermissions.EditContent, personPage);

            return $"Is authorized? {isAuthorized}";
        }

        public async Task<string> CustomAuthorization()
        {
            var isAuthorized = await _authorizerService.AuthorizeAsync(User, PersonPagePermissions.ManagePersonPages);

            return $"Is authorized? {isAuthorized}";
        }
    }
}
