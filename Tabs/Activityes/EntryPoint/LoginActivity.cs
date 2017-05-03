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
using Tabs.Activityes.Documents;

namespace Tabs.Activityes.EntryPoint
{
    class LoginActivity : Fragment
    {
        private int count = 0;
        private int position;
      
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.login, container, false);

            var button = root.FindViewById<Button>(Resource.Id.auth_btn);

            button.Click += StartNewActivity;

            ViewCompat.SetElevation(root, 50);
            return root;
        }

        public static LoginActivity NewInstance(int position)
        {
            var f = new LoginActivity();
            var b = new Bundle();

            b.PutInt("position", position);
            f.Arguments = b;

            return f;
        }

        /*protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);

            var button = FindViewById<Button>(Resource.Id.button);
            field = FindViewById<TextView>(Resource.Id.textView1);

            field.Text = "Количество кликов - " + count;

            button.Click += (object sender, EventArgs e) =>
            {
                setText();
            };
        }*/

        void StartNewActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(DocumentsActivity));
            StartActivity(intent);
        }
    }
}