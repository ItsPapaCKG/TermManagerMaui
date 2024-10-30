
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace C971_Grant_Putnam.Models
{
    public class DatabaseService
    {
        public enum Tables
        {
            COURSE,
            TERM,
            ASSESSMENT
        }

        private SQLiteAsyncConnection conn;
        public async Task Init()
        {
            // Initialize Database if it doesn't already exist
            if (conn != null)
                return;

            var dbfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WGU.db");

            conn = new SQLiteAsyncConnection(dbfilepath);

            await conn.CreateTableAsync<Course>().ConfigureAwait(false);
            await conn.CreateTableAsync<Term>().ConfigureAwait(false);
            await conn.CreateTableAsync<Assessment>().ConfigureAwait(false);
        }

        public async Task<ObservableCollection<Term>> GetTerms()
        {
            await Init().ConfigureAwait(false);

            var d = await conn.Table<Term>().ToListAsync().ConfigureAwait(false);
            var data = new ObservableCollection<Term>(d);

            return data;
        }

        public async Task AddTerm(string name, DateTime start, DateTime end, bool notify)
        {
            await Init().ConfigureAwait(false);

            var t = new Term { Name = name, Start = start, End = end, Notify = notify };

            await conn.InsertAsync(t).ConfigureAwait(false);
        }
        public async Task RemoveTerm(int termId)
        {
            await Init().ConfigureAwait(false);

            await conn.DeleteAsync(termId).ConfigureAwait(false);
        }

        public async Task UpdateTerm(int id, string name, DateTime start, DateTime end, bool notify)
        {
            await Init().ConfigureAwait(false);

            var termQuery = await conn.Table<Term>().FirstOrDefaultAsync(t => t.Id == id).ConfigureAwait(false);

            if (termQuery != null)
            {
                termQuery.Name = name;
                termQuery.Start = start;
                termQuery.End = end;
                termQuery.Notify = notify;

                await conn.UpdateAsync(termQuery).ConfigureAwait(false);
            }
        }

        public async Task<ObservableCollection<Course>> GetCourses()
        {
            await Init().ConfigureAwait(false);

            var d = await conn.Table<Course>().ToListAsync().ConfigureAwait(false);
            var data = new ObservableCollection<Course>(d);

            return data;
        }

        public async Task<ObservableCollection<Course>> GetCourses(int termId)
        {
            await Init().ConfigureAwait(false);

            var query = await conn.Table<Course>().Where(c => c.TermId == termId).ToListAsync().ConfigureAwait(false);
            var q = new ObservableCollection<Course>(query);

            return q;
        }

        public async Task<Course> GetCourse(int courseId)
        {
            await Init().ConfigureAwait(false);

            // refactor
            var query = await conn.QueryAsync<Course>("SELECT * FROM course WHERE Id = ?", courseId).ConfigureAwait(false);

            return query[0];
        }

        public async Task AddCourse(string name, DateTime start, DateTime end, bool notify, string status, string instructorName, string instructorPhone, string instructorEmail, string notes)
        {
            await Init().ConfigureAwait(false);

            var c = new Course { Name = name, Start = start, End = end, Notify = notify, Status = status, Instructor_Name = instructorName, Instructor_Phone = instructorPhone, Instructor_Email = instructorEmail, Notes = notes };

            await conn.InsertAsync(c).ConfigureAwait(false);
        }
        public async Task AddCourse(Course course)
        {
            await Init().ConfigureAwait(false);

            await conn.InsertAsync(course).ConfigureAwait(false);
        }
        public async Task RemoveCourse(Course course)
        {
            await Init().ConfigureAwait(false);

            await conn.DeleteAsync(course).ConfigureAwait(false);
        }

        public async Task UpdateCourse(int id, string name, DateTime start, DateTime end, bool notify, string status, string instructorName, string instructorPhone, string instructorEmail, string notes)
        {
            await Init().ConfigureAwait(false);

            var courseQuery = await conn.Table<Course>().FirstOrDefaultAsync(t => t.Id == id).ConfigureAwait(false);

            if (courseQuery != null)
            {
                courseQuery.Name = name;
                courseQuery.Start = start;
                courseQuery.End = end;
                courseQuery.Notify = notify;
                courseQuery.Status = status;
                courseQuery.Instructor_Email = instructorEmail;
                courseQuery.Instructor_Phone = instructorPhone;
                courseQuery.Instructor_Name = instructorName;
                courseQuery.Notes = notes;

                await conn.UpdateAsync(courseQuery).ConfigureAwait(false);
            }
        }

        public async Task UpdateCourse(int id, Course course)
        {
            await Init().ConfigureAwait(false);

            try
            {
                var courseQuery = await conn.Table<Course>().FirstOrDefaultAsync(t => t.Id == id).ConfigureAwait(false);

                if (courseQuery != null)
                {
                    courseQuery.Name = course.Name;
                    courseQuery.Start = course.Start;
                    courseQuery.End = course.End;
                    courseQuery.Notify = course.Notify;
                    courseQuery.Status = course.Status;
                    courseQuery.Instructor_Email = course.Instructor_Email;
                    courseQuery.Instructor_Phone = course.Instructor_Phone;
                    courseQuery.Instructor_Name = course.Instructor_Name;
                    courseQuery.Notes = course.Notes;

                    await conn.UpdateAsync(courseQuery).ConfigureAwait(false);
                }
                else
                {
                    throw new Exception($"Cannot find course with id {course.Id}");
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

        }

        public async Task<ObservableCollection<Assessment>> GetAssessments()
        {
            await Init().ConfigureAwait(false);

            var query = await conn.Table<Assessment>().ToListAsync().ConfigureAwait(false);
            var q = new ObservableCollection<Assessment>(query);

            return q;
        }

        public async Task<ObservableCollection<Assessment>> GetAssessments(int courseId)
        {
            await Init().ConfigureAwait(false);

            var query = await conn.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync().ConfigureAwait(false);
            var q = new ObservableCollection<Assessment>(query);

            return q;
        }

        public async Task AddAssessment(Assessment a)
        {
            await Init().ConfigureAwait(false);

            await conn.InsertAsync(a).ConfigureAwait(false);
        }

        public async Task AddAssessment(string name, DateTime start, DateTime end, string type, int courseId, bool notify)
        {
            await Init().ConfigureAwait(false);

            var a = new Assessment { Notify = notify, Name = name, Start = start, End = end, Type = type, CourseId = courseId };

            await conn.InsertAsync(a).ConfigureAwait(false);
        }
        public async Task RemoveAssessment(int id)
        {
            await Init().ConfigureAwait(false);

            await conn.DeleteAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateAssessment(int id, Assessment assessment)
        {
            await Init().ConfigureAwait(false);

            var assessmentQuery = await conn.Table<Assessment>().FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);

            if (assessmentQuery != null)
            {
                assessmentQuery.Name = assessment.Name;
                assessmentQuery.Start = assessment.Start;
                assessmentQuery.End = assessment.End;
                assessmentQuery.Type = assessment.Type;
                assessmentQuery.CourseId = assessment.CourseId;
                assessmentQuery.Notify = assessment.Notify;

                await conn.UpdateAsync(assessmentQuery).ConfigureAwait(false);
            }
        }

        public async Task UpdateAssessment(int id, string name, DateTime start, DateTime end, string type, int courseId, bool notify)
        {
            await Init().ConfigureAwait(false);

            var assessmentQuery = await conn.Table<Assessment>().FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);

            if (assessmentQuery != null)
            {
                assessmentQuery.Name = name;
                assessmentQuery.Start = start;
                assessmentQuery.End = end;
                assessmentQuery.Type = type;
                assessmentQuery.CourseId = courseId;
                assessmentQuery.Notify = notify;

                await conn.UpdateAsync(assessmentQuery).ConfigureAwait(false);
            }
        }

        public async Task LoadSampleData()
        {
            await Init();

            try
            {
                await conn.RunInTransactionAsync(conn =>
                {
                    conn.Execute("DELETE FROM term;");
                    conn.Execute("DELETE FROM course;");
                    conn.Execute("DELETE FROM assessment;");

                    conn.Execute("UPDATE sqlite_sequence SET seq = 0 WHERE name = 'term';");
                    conn.Execute("UPDATE sqlite_sequence SET seq = 0 WHERE name = 'course';");
                    conn.Execute("UPDATE sqlite_sequence SET seq = 0 WHERE name = 'assessment';");
                });

                await conn.ExecuteAsync("VACUUM;");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("---Failed to clear Database!---");
            }

            try
            {
                var terms = new[]
                    {
                    new Term { Name = "Spring Term", Start = DateTime.Now, End = DateTime.Now, Notify = false },
                    new Term { Name = "Summer Term", Start = new DateTime(2024, 12, 04), End = DateTime.Now, Notify = false },
                    new Term { Name = "Fall Term", Start = DateTime.Now, End = DateTime.Now, Notify = false },
                    new Term { Name = "Winter Term", Start = DateTime.Now, End = DateTime.Now, Notify = false }
                    };

                var courses = new[] {
                    new Course { Name = "Basket Weaving", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 1, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774", Notes = "This is a test note. It talks all about how cool we are in this app. Imagine being able to take notes on a specific class and have it categorized correctly.. Neat, right?" },
                    new Course { Name = "Scuba Diving", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 1, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Calculus I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 1, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Cat Class I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 1, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Woodworking", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 1, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Banking", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 1, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Beaching I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 2, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Surfing I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 2, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Cats I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 2, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Pumpkins I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 3, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Leaves I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 3, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Bank Fraud I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 3, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "TestTaking III", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 4, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Bus Driving I", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 4, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" },
                    new Course { Name = "Life II", Start = new DateTime(2024, 11, 1), End = new DateTime(2024, 11, 30), Status = "Starting Soon", TermId = 4, Instructor_Name = "Bob Good", Instructor_Email = "BobGood@gmail.com", Instructor_Phone = "239-285-6774" }
                    };

                var assessments = new[]
                {
                    new Assessment { CourseId = 1, Name = "DEA01", Start = new DateTime(2024,12,04), End = new DateTime(2024,12,05), Notify = true, Type = "PA" },
                    new Assessment { CourseId = 1, Name = "DEA10", Start = new DateTime(2024,12,06), End = new DateTime(2024,12,07), Notify = false, Type = "OA" }
                };

                await conn.RunInTransactionAsync(conn => {

                    foreach (var term in terms)
                    {
                        conn.Insert(term);
                    }

                    foreach (var course in courses)
                    {
                        conn.Insert(course);
                    }

                    foreach (var assessment in assessments)
                    {
                        conn.Insert(assessment);
                    }
                });
            } catch (Exception ex) { Debug.WriteLine(ex); Debug.WriteLine("---Failed to load database.---"); }

        }
    }
}
