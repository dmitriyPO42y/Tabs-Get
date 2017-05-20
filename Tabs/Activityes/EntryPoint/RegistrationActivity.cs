using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Support.V4.View;
using Tabs.dataBase.Actions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Widget;
using Tabs.Activityes.Documents;
using Tabs.dataBase.Users;

namespace Tabs.Activityes.EntryPoint
{
    class RegistrationActivity : Fragment
    {
        private static DataBase db;
        private int position;
        private User user;

        Button regBtn;
        EditText loginTxt;
        EditText emailTxt;
        EditText passwordTxt;
        EditText confirmPasswordTxt;
        TextView regErrorMess;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.registration, container, false);
            ViewCompat.SetElevation(root, 50);

            regBtn = root.FindViewById<Button>(Resource.Id.reg_btn);
            loginTxt = root.FindViewById<EditText>(Resource.Id.reg_login);
            emailTxt = root.FindViewById<EditText>(Resource.Id.reg_mail);
            passwordTxt = root.FindViewById<EditText>(Resource.Id.reg_pass);
            confirmPasswordTxt = root.FindViewById<EditText>(Resource.Id.reg_conf_pass);
            regErrorMess = root.FindViewById<TextView>(Resource.Id.regErrorView);

            regBtn.Click += addNewUser;

            return root;
        }

        private void addNewUser(object sender, EventArgs e)
        {
            if (passwordTxt.Text.Equals(confirmPasswordTxt.Text))
            {
                user = new User();
                user.login = loginTxt.Text;
                user.password = passwordTxt.Text;
                user.email = emailTxt.Text;

                var check = db.addUser(user);

                if (check == -1)
                {
                    regErrorMess.Text = "Добавлен новый пользователь";
                }

                if (check == 1)
                {
                    regErrorMess.Text = "Логин занят";
                }

                if (check == 2)
                {
                    regErrorMess.Text = "Email занят";
                }
            }
            else
            {
                regErrorMess.Text = "Пароли не совпадают";
            }            
        }

        public static RegistrationActivity NewInstance(int position, DataBase dataBase)
        {
            db = dataBase;

            var f = new RegistrationActivity();
            var b = new Bundle();

            b.PutInt("position", position);
            f.Arguments = b;

            return f;
        }
    }
}