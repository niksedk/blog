using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SubIt.Features.Subtitle.Convert.Controllers
{
    public class ToDo
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Convert")]
    public class ConvertController : Controller
    {

        [HttpPost]
        public IActionResult Convert(string to, string encoding)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                Request.Body.CopyTo(ms);
                bytes = ms.ToArray();
            }

            if (bytes == null || bytes.Length < 10)
            {
                return BadRequest(new { error = "No body found or too short" } );
            }

            //auto detect format...

            var enc = Encoding.UTF8;
            return File(new MemoryStream(bytes), "application/octet-stream", "test.txt");
        }

        [HttpPost]
        public IActionResult ConvertToPlainText(string to, string encoding)
        {
            var bytes = Encoding.UTF8.GetBytes("Hallo world!");
            var ms = new MemoryStream(bytes);
            return File(ms, "application/octet-stream", "test.txt");
        }


        //[HttpGet("{id}", Name = "GetTodo")]
        //public IActionResult GetById(string id)
        //{
        //    return new ObjectResult(new ToDo { Id = id, Text ="yo"} );
        //}
    }




}