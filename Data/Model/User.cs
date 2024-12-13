using System.ComponentModel.DataAnnotations;

namespace WebData.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "Name cannot be longer than 256 characters")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "UserName cannot be longer than 256 characters")]
        public string UserName { get; set; }

        [StringLength(250)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }
        public virtual Role? Role { get; set; }

    }
}
