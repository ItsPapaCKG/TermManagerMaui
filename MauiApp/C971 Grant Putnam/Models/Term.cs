using SQLite;


namespace C971_Grant_Putnam.Models
{
    [Table("term")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public bool Notify { get; set; }
    }
}
