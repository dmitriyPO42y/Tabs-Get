using System;
using Android.Content;
using Android.Preferences;
using Tabs.dataBase.Users;

namespace Tabs.Core.Preferences
{
    class AppPreferences
    {
        private ISharedPreferences nameSharedPrefs;
        private ISharedPreferencesEditor namePrefsEditor; //Declare Context,Prefrences name and Editor name  
        private Context mContext;
        private static string LOGIN_KEY = "login";
        private static string PASSWORD_KEY = "password";
        private static string EMAIL_KEY = "email";
        private static string ID_KEY = "id";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            nameSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            namePrefsEditor = nameSharedPrefs.Edit();
        }
        /// <summary>
        /// Сохраняем информацию о текущем пользователе
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        public void saveUserSession(int id, string login, string password, string email) // Save data Values  
        {
            namePrefsEditor.PutString(LOGIN_KEY, login);
            namePrefsEditor.PutString(PASSWORD_KEY, password);
            namePrefsEditor.PutString(EMAIL_KEY, password);
            namePrefsEditor.PutInt(ID_KEY, id);
            namePrefsEditor.Commit();
        }
        /// <summary>
        /// Возвращаем информацию о текущем пользователе
        /// </summary>
        /// <returns></returns>
        public User getUser() 
        {
            return new User
            {
                login = nameSharedPrefs.GetString(LOGIN_KEY, ""),
                password = nameSharedPrefs.GetString(PASSWORD_KEY, ""),
                email = nameSharedPrefs.GetString(EMAIL_KEY, ""),
                id = nameSharedPrefs.GetInt(ID_KEY, -1)
            };
        }
        /// <summary>
        /// Удаляем информацию о текущем пользователе и закрываем сессию
        /// </summary>
        public void removeUserSession()
        {
            namePrefsEditor.Remove(ID_KEY).Remove(LOGIN_KEY).Remove(PASSWORD_KEY).Remove(EMAIL_KEY).Commit();
        }


    }
}
