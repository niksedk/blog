using System;

namespace Blog.Features.Blog.ViewModels
{
    public class BlogCommentViewModel
    {
        public int BlogCommentId { get; set; }
        public string Body { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
