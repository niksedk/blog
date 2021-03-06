﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Data.Security;

namespace Blog.Data.Blog
{
    public class BlogComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BlogCommentID")]
        public int BlogCommentId { get; set; }

        public string Body { get; set; }
        public BlogUser CreatedBy { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
