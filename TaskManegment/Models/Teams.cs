using System.ComponentModel.DataAnnotations;

namespace TaskManegment.Models
{
    public class Teams
    {
        [Key]
        public string TeamName { get; set; }
    }
}
