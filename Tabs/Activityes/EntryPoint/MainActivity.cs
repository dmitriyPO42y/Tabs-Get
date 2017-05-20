using Android.App;
using Android.OS;
using System.IO;
using Java.Lang;
using Android.Support.V4.App;
using com.refractored;
using Android.Support.V4.View;
using Android.Support.V7.App;
using SQLite;
using Tabs.dataBase.Actions;

namespace Tabs.Activityes.EntryPoint
{
    [Activity(Label = "Tabs", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        static DataBase db;
        
        MyAdapter adapter;
        ViewPager pager;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            db = new DataBase();
            
            adapter = new MyAdapter(SupportFragmentManager);
            pager = FindViewById<ViewPager>(Resource.Id.pager);
            PagerSlidingTabStrip tabs = (PagerSlidingTabStrip)FindViewById(Resource.Id.tabs);
            pager.Adapter = adapter;

            tabs.SetViewPager(pager);
            tabs.SetBackgroundColor(Android.Graphics.Color.Rgb(75,121,187));
        }

        public class MyAdapter : FragmentPagerAdapter
        {
            int tabCount = 2;

            public MyAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
            { }

            public override int Count
            {
                get
                {
                    return tabCount;
                }
            }

            private string[] titles = {"Вход", "Регистрация"};
            
            public override ICharSequence GetPageTitleFormatted(int position)
            {
                ICharSequence cs;
                cs = new Java.Lang.String(titles[position]);
                return cs;
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                if (position == 0)
                    return LoginActivity.NewInstance(position, db);
                else
                    return RegistrationActivity.NewInstance(position, db);
            }
        }
    }
}

