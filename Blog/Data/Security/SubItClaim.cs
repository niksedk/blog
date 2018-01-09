using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Security
{
    public class SubItClaim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ClaimID")]
        public int SubItClaimId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
