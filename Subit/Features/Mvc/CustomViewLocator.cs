using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace SubIt.Features.Mvc
{
    public class CustomViewLocator : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        { }

        /// <summary>
        /// Replace MVC standard "Views" folder with a more feature friendly "Features/Mvc/Views"
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return viewLocations.Select(f => f.Replace("/Views/", "/Features/Mvc/Views/"));
        }
    }
}