using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Log
{
    public class Referrer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ReferrerID")]
        public int ReferrerId { get; set; }
        public string Ref { get; set; }
        public DateTime Created { get; set; }
    }
}
