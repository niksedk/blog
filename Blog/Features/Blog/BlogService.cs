using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blog.Data;
using Blog.Data.Blog;
using Blog.Data.Security;
using Microsoft.EntityFrameworkCore;

namespace Blog.Features.Blog
{
    public class BlogService : IBlogService
    {
        private readonly SubItContext _context;
        public BlogService(SubItContext context)
        {
            _context = context;
        }

        public BlogComment AddComment(SubItUser user, int blogEntryId, string email, string ipAddress, string body)
        {
            var blogEntry = _context.BlogEntries.Include(blog => blog.Comments).FirstOrDefault(p => p.BlogEntryId == blogEntryId && !p.CommentsDisabled);
            if (blogEntry == null)
                throw new ArgumentException("Blog entry not found or comments is disabled", nameof(blogEntryId));

            var comment = new BlogComment()
            {
                Body = body,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                IpAddress = ipAddress,
                Email = email,
                CreatedBy = user
            };
            blogEntry.Comments.Add(comment);
            blogEntry.CommentCount = blogEntry.Comments.Count();
            _context.SaveChanges();
            return comment;
        }

        public bool Delete(SubItUser user, int blogEntryId)
        {
            var blogEntry = _context.BlogEntries.Include(blog => blog.Comments).FirstOrDefault(p => p.BlogEntryId == blogEntryId);
            if (blogEntry == null)
                return false;

            blogEntry.Comments.Clear();
            _context.BlogEntries.Remove(blogEntry);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteComment(SubItUser user, int blogCommentId)
        {
            var blogEntry = _context.BlogEntries.Include(blog => blog.Comments).FirstOrDefault(p => p.Comments.Any(c => c.BlogCommentId == blogCommentId));
            if (blogEntry == null)
                throw new ArgumentException("Blog entry not found for comment id", nameof(blogCommentId));
            var blogComment = blogEntry.Comments.FirstOrDefault(p => p.BlogCommentId == blogCommentId);
            if (blogComment == null)
                return false;

            blogEntry.Comments.Remove(blogComment);
            blogEntry.CommentCount = blogEntry.Comments.Count;
            _context.SaveChanges();
            return true;
        }

        public BlogEntry GetFull(string urlFriendlyId)
        {
            var blogEntry = _context.BlogEntries.Include(blog => blog.Comments).Include(blog => blog.CreatedBy).FirstOrDefault(p => p.UrlFriendlyId == urlFriendlyId);
            if (blogEntry == null)
                throw new ArgumentException("Blog entry not found", nameof(urlFriendlyId));

            return blogEntry;
        }

        public List<BlogComment> ListComments(int blogEntryId)
        {
            var blogEntry = _context.BlogEntries.Include(p => p.CreatedBy).FirstOrDefault(p => p.BlogEntryId == blogEntryId);
            if (blogEntry == null)
                throw new ArgumentException("Blog entry not found", nameof(blogEntryId));

            return blogEntry.Comments.OrderBy(p => p.Created).ToList();
        }

        public List<BlogComment> ListComments()
        {
            return _context.BlogComments.OrderBy(p => p.Created).ToList();
        }

        public List<BlogEntry> ListRecent(int fromDaysBack, int toDaysBack)
        {
            var to = DateTime.UtcNow.AddDays(-Math.Min(fromDaysBack, toDaysBack));
            var from = DateTime.UtcNow.AddDays(-Math.Max(fromDaysBack, toDaysBack));
            return _context.BlogEntries.Include(p => p.CreatedBy).Include(p => p.Comments).Where(p => p.Created >= from && p.Created <= to).OrderByDescending(p => p.Created).ToList();
        }

        public BlogEntry Add(SubItUser user, string title, string body, bool commentsDisabled)
        {
            var urlFriendlyId = GenerateUrlFriendlyId(title);
            if (_context.BlogEntries.Any(p => p.UrlFriendlyId == urlFriendlyId))
                urlFriendlyId += "_";
            if (_context.BlogEntries.Any(p => p.UrlFriendlyId == urlFriendlyId))
                urlFriendlyId += Guid.NewGuid().ToString();
            var blogEntry = new BlogEntry
            {
                Body = body,
                Title = title,
                UrlFriendlyId = urlFriendlyId,
                CommentsDisabled = commentsDisabled,
                CreatedBy = user,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };
            _context.BlogEntries.Add(blogEntry);
            _context.SaveChanges();
            return blogEntry;
        }

        private string GenerateUrlFriendlyId(string title)
        {
            var sb = new StringBuilder(title.Length);
            foreach (var ch in title)
            {
                if (ch == ' ' || ch == '.')
                    sb.Append('-');
                else if ("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_".Contains(ch.ToString()))
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        public BlogEntry Update(SubItUser user, int blogEntryId, string title, string body, bool commentsDisabled)
        {
            var blogEntry = _context.BlogEntries.Include(p => p.CreatedBy).FirstOrDefault(p => p.BlogEntryId == blogEntryId);
            if (blogEntry == null)
                throw new ArgumentException("Blog entry not found", nameof(blogEntryId));

            blogEntry.Title = title;
            blogEntry.Body = body;
            blogEntry.CommentsDisabled = commentsDisabled;
            blogEntry.Modified = DateTime.UtcNow;

            _context.SaveChanges();
            return blogEntry;
        }

        public BlogComment UpdateComment(SubItUser user, int blogCommentId, string body)
        {
            var blogComment = _context.BlogComments.Include(p => p.CreatedBy).FirstOrDefault(p => p.BlogCommentId == blogCommentId);
            if (blogComment == null)
                throw new ArgumentException("Blog comment not found", nameof(blogCommentId));

            blogComment.Body = body;
            blogComment.Modified = DateTime.UtcNow;

            return blogComment;
        }
    }
}
