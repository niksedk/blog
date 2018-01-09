﻿using System.Collections.Generic;
using Blog.Data.Blog;
using Blog.Features.Blog.ViewModels;

namespace Blog.Features.Blog.Factories
{
    public static class BlogEntryViewModelFactory
    {
        public static BlogEntryViewModel Make(BlogEntry blogEntry)
        {
            return new BlogEntryViewModel
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
        }

        internal static List<BlogEntryViewModel> Make(List<BlogEntry> blogEntries)
        {
            var list = new List<BlogEntryViewModel>(blogEntries.Count);
            foreach (var blogEntry in blogEntries)
            {
                list.Add(Make(blogEntry));
            }
            return list;
        }
    }
}
