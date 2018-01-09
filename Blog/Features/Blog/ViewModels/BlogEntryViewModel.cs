using System;
using System.Collections.Generic;

namespace Blog.Features.Blog.ViewModels
{
    public class BlogEntryViewModel
    {
        public int BlogEntryId { get; set; }
        public string UrlFriendlyId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public bool CommentsDisabled { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public int CommentCount { get; set; }

        public ICollection<BlogCommentViewModel> Comments { get; set; }
    }
}
