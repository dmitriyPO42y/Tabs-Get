using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using Widgets = Android.Widget;
using Android.Content;
using Tabs.Core.Preferences;
using Tabs.Activityes.EntryPoint;

namespace Tabs.Activityes.Documents
{
    [Activity(MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class DocumentsActivity : AppCompatActivity
    {
        private Widgets.FrameLayout mFragmentContainer;
        private SupportFragment mCurrentFragment;
        private Fragment_Local mFragment_local;
        private Fragment_Recent mFragment_recent;
        private Fragment_Profile mFragment_profile;
        private Fragment_Storage mFragment_storage;
        private Fragment_About mFragment_about;
        private Stack<SupportFragment> mStackFragments;
        private DrawerLayout drawerLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Создаем и добавляем фрагменты (рабочие страницы для каждого пункта меню)
            CreateFragments("tttt.pdf");

            // Устанавливаем вьюху
            SetContentView(Resource.Layout.drawer_layout);

            // Создаем и добавляем ToolBar
            CreateToolbar();

            // TODO Придумать изящней
            //this.Title = GetString(Resource.String.nav_recent);
            //ShowFragment(mFragment_recent);
            
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_recent):
                    this.Title = GetString(Resource.String.nav_recent);
                    ShowFragment(mFragment_recent);
                    break;
                case (Resource.Id.nav_local):
                    this.Title = GetString(Resource.String.nav_local);
                    ShowFragment(mFragment_local);
                    break;
                case (Resource.Id.nav_storage):
                    this.Title = GetString(Resource.String.nav_storage);
                    ShowFragment(mFragment_storage);
                    break;
                case (Resource.Id.nav_profile):
                    this.Title = GetString(Resource.String.nav_profile);
                    ShowFragment(mFragment_profile);
                    break;
                case (Resource.Id.nav_about):
                    this.Title = GetString(Resource.String.nav_about);
                    ShowFragment(mFragment_about);
                    break;
                case (Resource.Id.nav_exit):
                    Context mContext = Android.App.Application.Context;
                    AppPreferences app = new AppPreferences(mContext);
                    app.removeUserSession();
                    Intent intent = new Intent(this , typeof(MainActivity));
                    StartActivity(intent);
                    break;
            }

             // Close drawer
             drawerLayout.CloseDrawers();
        }

        #region Fragments

        private void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
            {
                return;
            }

            var trans = SupportFragmentManager.BeginTransaction();
            
            fragment.View.BringToFront();
            mCurrentFragment.View.BringToFront();

            trans.Hide(mCurrentFragment);
            trans.Show(fragment);

            trans.AddToBackStack(null);
            mStackFragments.Push(mCurrentFragment);
            trans.Commit();

            mCurrentFragment = fragment;
        }

        private void CreateFragments(string fileName = null) // по умолчанию файл не определен
        {
            mFragmentContainer = FindViewById<Widgets.FrameLayout>(Resource.Id.fragmentContainer);

            mFragment_local = new Fragment_Local();
            mFragment_recent = new Fragment_Recent();
            mFragment_about = new Fragment_About();
            mFragment_profile = new Fragment_Profile();
            mFragment_storage = new Fragment_Storage();

            if (!string.IsNullOrEmpty(fileName))
            {
                mFragment_local.fileName = fileName;
            }

            mStackFragments = new Stack<SupportFragment>();

            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, mFragment_about, "mFragment_about");
            trans.Hide(mFragment_about);

            trans.Add(Resource.Id.fragmentContainer, mFragment_profile, "mFragment_profile");
            trans.Hide(mFragment_profile);

            trans.Add(Resource.Id.fragmentContainer, mFragment_storage, "mFragment_storage");
            trans.Hide(mFragment_storage);

            trans.Add(Resource.Id.fragmentContainer, mFragment_local, "mFragment_local");
            trans.Hide(mFragment_local);

            trans.Add(Resource.Id.fragmentContainer, mFragment_recent, "mFragment_recent");
            trans.Commit();

            mCurrentFragment = mFragment_recent;
        }

        #endregion

        #region Toolbar

        private void CreateToolbar()
        {
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            toolbar.SetBackgroundColor(Android.Graphics.Color.Rgb(75, 121, 187));

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;


            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);

            drawerToggle.SyncState();
        }

        #endregion
    }
}