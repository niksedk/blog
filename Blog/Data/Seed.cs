using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Data.Blog;
using Blog.Data.Security;
using Microsoft.AspNetCore.Identity;

namespace Blog.Data
{
    public class Seed
    {
        public static void Initialize(BlogDbContext context, IPasswordHasher<BlogUser> passwordHasher)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                // Users has not been seeded
                var user = new BlogUser
                {
                    Name = "Admin",
                    Email = "no@email.com",
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,                    
                    Claims = new List<BlogClaim>
                    {
                        new BlogClaim { Key = "role", Value = "admin" },
                        new BlogClaim { Key = "role", Value = "god" },
                        new BlogClaim { Key = "role", Value = "master" },
                    }
                };
                user.PasswordHash = passwordHasher.HashPassword(user, "password");
                context.Users.Add(user);
                context.SaveChanges();
            }

            if (!context.BlogEntries.Any())
            {
                // Blog entries has not been seeded
                var user = context.Users.First();
                for (int i = 1; i < 10; i++)
                {
                    var blogEntry = new BlogEntry
                    {
                        CreatedBy = user,
                        Title = "Welcome no" + i,
                        Body = "This is the an auto seeded blog post.",
                        Created = DateTime.UtcNow.AddDays(-i),
                        Modified = DateTime.UtcNow.AddDays(-i),
                        UrlFriendlyId = "welcome-no-" + i,
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {  Body = "First", Name= $"Benny {i}",  Email = "no@email.com", IpAddress  = "127.0.0.1", Created = DateTime.UtcNow, Modified = DateTime.UtcNow }
                        }
                    };
                    blogEntry.CommentCount = blogEntry.Comments.Count;
                    context.BlogEntries.Add(blogEntry);
                    context.SaveChanges();
                }
            }
        }
    }
}
