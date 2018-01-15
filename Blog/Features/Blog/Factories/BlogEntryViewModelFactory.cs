using System.Collections.Generic;
using Blog.Data.Blog;
using Blog.Features.Blog.ViewModels;

namespace Blog.Features.Blog.Factories
{
    public static class BlogEntryViewModelFactory
    {
        public static BlogEntryViewModel Make(BlogEntry blogEntry)
        {
            var vm = new BlogEntryViewModel
            {
                Name = blogEntry.CreatedBy.Name,
                Email = blogEntry.CreatedBy.Email,
                BlogEntryId = blogEntry.BlogEntryId,
                Body = blogEntry.Body,
                CommentCount = blogEntry.CommentCount,
                Comments = BlogCommentViewModelFactory.Make(blogEntry.Comments),
                CommentsDisabled = blogEntry.CommentsDisabled,
                Created = blogEntry.Created,
                Modified = blogEntry.Modified,
                Title = blogEntry.Title,
                UrlFriendlyId = blogEntry.UrlFriendlyId,
                UserId = blogEntry.CreatedBy.UserId
            };        
            return vm;
        }

        internal static List<BlogEntryViewModel> Make(List<BlogEntry> blogEntries)
        {
            if (blogEntries == null)
            {
                return new List<BlogEntryViewModel>();
            }

            var list = new List<BlogEntryViewModel>(blogEntries.Count);
            foreach (var blogEntry in blogEntries)
            {
                list.Add(Make(blogEntry));
            }
            return list;
        }
    }
}
