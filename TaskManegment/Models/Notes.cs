using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManegment.Models
{
    public class Notes: UserActivity
    {
        [Key]
        public int NotesId { get; set; }

      
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]

        public Tasks Tasks { get; set; }

        [Required]
        [StringLength(1000)]
        public string NoteText { get; set; }
    }
}
