using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManegment.Models
{
    public class Attachments: UserActivity
    {
        [Key]
        public int AttachmentId { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Tasks Tasks { get; set; }
        public string FilePath { get; set; }



        
    }
}
