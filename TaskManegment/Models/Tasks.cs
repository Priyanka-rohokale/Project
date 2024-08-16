using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TaskManegment.Models
    {
        public class Tasks: UserActivity
        {
        [Key]
        public int TaskId { get; set; }

        public string TaskName { get; set; }

            public string Description { get; set; }

            public string Status { get; set; }

            public DateTime DueDate { get; set; }

 
        }
}
