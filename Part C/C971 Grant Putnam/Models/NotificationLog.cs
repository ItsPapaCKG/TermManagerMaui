using SQLite;

namespace C971_Grant_Putnam.Models
{
    [Table("notification")]
    public class NotificationLog
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? AssessmentId { get; set; }
        public string Type { get; set; }
        public DateTime ScheduledFor { get; set; }

    }
}
