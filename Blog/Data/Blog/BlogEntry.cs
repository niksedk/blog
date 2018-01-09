using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Data.Security;

namespace Blog.Data.Blog
{
    public class BlogEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BlogEntryID")]
        public int BlogEntryId { get; set; }

        /// <summary>
        /// For url routning - some version of the title
        /// </summary>
        public string UrlFriendlyId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public bool CommentsDisabled { get; set; }
        public SubItUser CreatedBy { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public int CommentCount { get; set; }

        public ICollection<BlogComment> Comments { get; set; }
    }
}
