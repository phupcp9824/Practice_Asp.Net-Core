using System.ComponentModel.DataAnnotations;

namespace WebData.Model
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        // virtual help performance optimization
        public virtual ICollection<User>? users { get; set;}
    }
}
