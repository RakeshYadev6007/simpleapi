using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

        
    }

    public class jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public string Subject { get; set; }
    }
}
