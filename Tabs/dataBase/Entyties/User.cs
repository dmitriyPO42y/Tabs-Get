using SQLite;

namespace Tabs.dataBase.Users
{
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }
        [MaxLength(25)]
        public string login { get; set; }
        [MaxLength(25)]
        public string email { get; set; }
        [MaxLength(15)]
        public string password { get; set; }
    }
}