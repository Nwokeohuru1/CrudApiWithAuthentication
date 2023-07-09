using System.ComponentModel.DataAnnotations;

namespace CrudApiWithAuthentication.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Delflag { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}
