namespace TaskManegment.Models
{
    public class UserActivity
    {
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? AssignedTo { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
