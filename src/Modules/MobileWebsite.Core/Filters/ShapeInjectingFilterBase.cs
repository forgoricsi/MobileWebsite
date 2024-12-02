using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MobileWebsite.Core.Filters
{
    public class ShapeInjectingFilterBase
    {
        public static Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!(context.Result is ViewResult || context.Result is PageResult))
            {
                return next();
            }
            return Task.CompletedTask;
        }
    }
}