
using SQLite;
using System.IO;

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
        public void Init()
        {
            // Initialize Database if it doesn't already exist
            if (conn != null)
                return;

            var dbfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WGU.db");

            conn = new SQLiteAsyncConnection(dbfilepath);

            conn.CreateTableAsync<Course>();
        }

        public async void GetAll(Tables table_enum)
        {
            if (table_enum == Tables.COURSE) { return; }
        }

    }
}
