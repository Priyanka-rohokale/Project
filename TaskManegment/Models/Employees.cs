using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManegment.Models
{
    public class Employees: UserActivity
    {
        [Key]
        public int EmpId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string TeamName { get; set; }

        [ForeignKey("TeamName")]
        public Teams Teams { get; set; }
    }
}
