using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Features.Shared
{
    public class SpaController : Controller
    {
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }

        public IActionResult Spa()
        {
            return File("~/index.html", "text/html");
        }
    }
}