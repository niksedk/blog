using Microsoft.AspNetCore.Mvc;

namespace Blog.Features.Shared
{
    /// <summary>
    /// Catch all redirects to root index.html file
    /// https://code.msdn.microsoft.com/How-to-fix-the-routing-225ac90f
    /// </summary>
    public class SpaController : Controller
    {
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}