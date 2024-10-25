
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
        }

        public async Task<IEnumerable<object>> GetTerms()
        {
            await Init();

            var data = await conn.Table<Term>().ToListAsync().ConfigureAwait(false);

            return data;
        }

        public async Task AddTerm(string name, DateTime start, DateTime end)
        {
            await Init();

            var t = new Term { Name = name, Start = start, End = end };

            await conn.InsertAsync(t).ConfigureAwait(false);
        }
        public async Task RemoveTerm(Term term)
        {
            await Init();

            await conn.DeleteAsync(term).ConfigureAwait(false);
        }
        public async Task<IEnumerable<Course>> GetCourses(int termId)
        {
            await Init();

            var query = await conn.Table<Course>().Where(c => c.TermId == termId).ToListAsync().ConfigureAwait(false);

            return query;
        }

        public async Task<Course> GetCourse(int courseId)
        {
            await Init();

            var query = await conn.QueryAsync<Course>("SELECT * FROM course WHERE Id = ?", courseId).ConfigureAwait(false);

            return query[0];
        }

        public async Task<IEnumerable<Assessment>> GetAssessments(int courseId)
        {
            await Init();

            var query = await conn.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync().ConfigureAwait(false);

            return query;
        }


    }
}
