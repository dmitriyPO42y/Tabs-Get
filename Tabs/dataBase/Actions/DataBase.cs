using System.IO;
using Mono.Data.Sqlite;
using Tabs.dataBase.Users;
using SQLite;

namespace Tabs.dataBase.Actions
{
    public class DataBase
    {
        private SQLiteConnection db;

        public DataBase()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
            db = new SQLiteConnection(dpPath);

            db.CreateTable<User>();
        }

        public bool checkUserForLogin(string login, string password)
        {
            var sataSet = db.Table<User>();

            var user = sataSet.Where(x => x.login == login && x.password == password).FirstOrDefault();

            if (user != null)
            {
                return true;
            }

            return false;
        }

        public int checkUserForRegistration(User user)
        {
            var dataSet = db.Table<User>();

            var info = dataSet.Where(x => x.login == user.login || x.email == user.email).FirstOrDefault();

            if (info != null && info.login == user.login)
            {
                return 1;
            }

            if (info != null && info.email == user.email)
            {
                return 2;
            }

            return -1;
        }

        public User getUser(string login)
        {
            var sataSet = db.Table<User>();

            return sataSet.Where(x => x.login == login).FirstOrDefault();

        }

        public int addUser(User user)
        {

            var check = this.checkUserForRegistration(user);
            if (check == -1)
            {
                db.Insert(user);
            }

            return check;
        }
    }
}