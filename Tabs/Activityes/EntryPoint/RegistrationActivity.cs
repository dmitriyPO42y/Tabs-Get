using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Support.V4.View;

namespace Tabs.Activityes.EntryPoint
{
    class RegistrationActivity : Fragment
    {
        private int position;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.registration, container, false);

            /*var field = root.FindViewById<TextView>(Resource.Id.textView1);

            field.Text = "Количество кликов - " + count;

            button.Click += (object sender, EventArgs e) =>
            {

                field.Text = "Количество кликов - " + count++;
                //setText();
            };*/

            ViewCompat.SetElevation(root, 50);
            return root;
        }

        public static RegistrationActivity NewInstance(int position)
        {
            var f = new RegistrationActivity();
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
    }
}