using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Preferences;
using Tabs.Activityes.Documents;
using Tabs.dataBase.Actions;
using Tabs.Core.Preferences;
using Tabs.dataBase.Users;

namespace Tabs.Activityes.EntryPoint
{
    class LoginActivity : Fragment
    {
        private int count = 0;
        private int position;

        private static DataBase db;

        private Context mContext;
        private AppPreferences app;

        Button loginBtn;
        EditText loginTxt;
        EditText passwordTxt;
        TextView errorTxt;
        TextView restore_pass_btn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");

            mContext = Android.App.Application.Context;

            app = new AppPreferences(mContext);
            User user = app.getUser();

            if (user.id != -1)
            {
                RedirectTo("Documents");
            }

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.login, container, false);

            loginBtn = root.FindViewById<Button>(Resource.Id.auth_btn);
            loginTxt = root.FindViewById<EditText>(Resource.Id.input_login);
            passwordTxt = root.FindViewById<EditText>(Resource.Id.input_password);
            errorTxt = root.FindViewById<TextView>(Resource.Id.errorText);
            restore_pass_btn = root.FindViewById<TextView>(Resource.Id.errorText);

            restore_pass_btn.Click += RestorePassword;
            loginBtn.Click += Login;

            ViewCompat.SetElevation(root, 50);
            return root;
        }

        public static LoginActivity NewInstance(int position, DataBase dataBase)
        {
            db = dataBase;

            var f = new LoginActivity();
            var b = new Bundle();

            b.PutInt("position", position);
            f.Arguments = b;

            return f;
        }

        void Login(object sender, EventArgs e)
        {
            if (db.checkUserForLogin(loginTxt.Text, passwordTxt.Text))
            {
                User user = db.getUser(loginTxt.Text);
                app.saveUserSession(user.id, user.login, user.password, user.email);
                RedirectTo("Documents");
            }
            else
            {
                errorTxt.Text = "Неверный логин или пароль";
            }

        }

        void RestorePassword(object sender, EventArgs e)
        {
            RedirectTo("Other");//TODO Доделать
        }

        void RedirectTo(string redirectTo)
        {
            Intent intent;
            if (redirectTo.Equals("Documents"))
            {
                intent = new Intent(this.Activity, typeof(DocumentsActivity));
            }
            else // TODO else if on RestorePass (redirectTo.Equals("RestorePass"))
            {
                intent = new Intent(this.Activity, typeof(DocumentsActivity));
            }
            StartActivity(intent);
        }

    }
}