using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Security
{
    public class SubItUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserID")]
        public int UserId { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime? DeletedTime { get; set; }
        public DateTime? LastLogin { get; set; }
        public string PasswordHash { get; set; }
        public int WrongPasswordAttempts { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public ICollection<SubItClaim> Claims { get; set; }
    }
}
