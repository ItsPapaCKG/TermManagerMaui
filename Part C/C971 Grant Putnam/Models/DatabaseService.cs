
using SQLite;
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

        public async Task<IEnumerable<object>> GetTerms()
        {
            await Init().ConfigureAwait(false);

            var data = await conn.Table<Term>().ToListAsync().ConfigureAwait(false);

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

        public async Task UpdateTerm(int id, string name, DateTime start, DateTime end)
        {
            await Init().ConfigureAwait(false);

            var termQuery = await conn.Table<Term>().FirstOrDefaultAsync(t => t.Id == id).ConfigureAwait(false);

            if (termQuery != null)
            {
                termQuery.Name = name;
                termQuery.Start = start;
                termQuery.End = end;

                await conn.UpdateAsync(termQuery).ConfigureAwait(false);
            }
        }
        public async Task<IEnumerable<Course>> GetCourses(int termId)
        {
            await Init().ConfigureAwait(false);

            var query = await conn.Table<Course>().Where(c => c.TermId == termId).ToListAsync().ConfigureAwait(false);

            return query;
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
        public async Task RemoveCourse(int courseId)
        {
            await Init().ConfigureAwait(false);

            await conn.DeleteAsync(courseId).ConfigureAwait(false);
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

        public async Task<IEnumerable<Assessment>> GetAssessments(int courseId)
        {
            await Init().ConfigureAwait(false);

            var query = await conn.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync().ConfigureAwait(false);

            return query;
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

        public async Task AssessmentQuery(int id, string name, DateTime start, DateTime end, string type, int courseId, bool notify)
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

        public async void LoadSampleData()
        {
            await Init();

            Term term1 = new Term { Name = "Term 1", Start = DateTime.Now, End = DateTime.Now, Notify = false };

            await conn.InsertAsync(term1).ConfigureAwait(false);
        }
    }
}
