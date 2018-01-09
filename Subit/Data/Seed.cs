using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using SubIt.Data.Blog;
using SubIt.Data.Security;

namespace SubIt.Data
{
    public class Seed
    {
        public static void Initialize(SubItContext context, IPasswordHasher<SubItUser> passwordHasher)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                // users has not been seeded
                var user = new SubItUser
                {
                    Name = "Nikolaj Olsson",
                    Email = "nikse.dk@gmail.com",
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,                    
                    Claims = new List<SubItClaim>
                    {
                        new SubItClaim { Key = "role", Value = "admin" }
                    }
                };
                user.PasswordHash = passwordHasher.HashPassword(user, "password");
                context.Users.Add(user);
                context.SaveChanges();
            }

            if (!context.BlogEntries.Any())
            {
                // blog entries has not been seeded

                for (int i = 1; i < 10; i++)
                {
                    var blogEntry = new BlogEntry
                    {
                        CreatedBy = context.Users.First(),
                        Title = "Welcome no" + i,
                        Body = "This is the an auto seeded blog post.",
                        Created = DateTime.UtcNow.AddDays(-i),
                        Modified = DateTime.UtcNow.AddDays(-i),
                        UrlFriendlyId = "welcome-no-" + i,
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {  Body = "First", Created = DateTime.UtcNow, Modified = DateTime.UtcNow }
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
