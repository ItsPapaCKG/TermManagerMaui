using SQLite;

namespace C971_Grant_Putnam.Models
{
    [Table("course")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }

        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Notify { get; set; }
        public string Instructor_Name { get; set; }
        public string Instructor_Phone { get; set; }
        public string Instructor_Email { get; set; }
        public string Notes { get; set; }
    }
}
