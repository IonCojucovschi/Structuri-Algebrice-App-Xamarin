﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using StructureAlgebrics.Manager;

namespace StructureAlgebrics.Pages
{
    [Activity(Label = "Structuri Algebrice", MainLauncher =true,Theme = "@style/MyTheme")]
    public class FirstPage : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private MenuActionManager mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ListView mRightDrawer;
        private ArrayAdapter mLeftAdapter;
        private ArrayAdapter mRightAdapter;
        private List<string> mLeftDataSet;
        private List<string> mRightDataSet;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            mRightDrawer = FindViewById<ListView>(Resource.Id.right_drawer);

            mLeftDrawer.Tag = 0;
            mRightDrawer.Tag = 1;

            SetSupportActionBar(mToolbar);

            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("    PARTEA PRACTICA");
            mLeftDataSet.Add("Proprietatile unui grup");
            mLeftDataSet.Add("Prop. prod. 2 grupuri");
            mLeftDataSet.Add("Izotopii grupului");
            mLeftDataSet.Add("Compunerea substitutiilor");
            mLeftDataSet.Add("Laboratorul 4");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            mRightDataSet = new List<string>();
            mRightDataSet.Add("      PARTEA TEORETICA");
            mRightDataSet.Add("Cap I");
            mRightDataSet.Add("Cap II");
            mRightDataSet.Add("Lucrari laborator ex.");
            mRightAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mRightDataSet);
            mRightDrawer.Adapter = mRightAdapter;

            mDrawerToggle = new MenuActionManager(
                this,                           //Host Activity
                mDrawerLayout,                  //DrawerLayout
                Resource.String.openDrawer,     //Opened Message
                Resource.String.closeDrawer     //Closed Message
            );

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();

            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.openDrawer);
                }

                else
                {
                    SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }
            }

            else
            {
                //This is the first the time the activity is ran
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }


            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
            mRightDrawer.ItemClick += MRightDrawer_ItemClick;

        }

        private void MRightDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string activityChoser = mRightDataSet[e.Position];

            var intent = new Intent();
            switch (activityChoser)
            {
                case "Cap I":
                    intent.SetClass(this, typeof(GroupProperty));
                    break;
                case "Cap II":
                    intent.SetClass(this, typeof(ProduceGroups));
                    break;
                case "Lucrari laborator ex.":
                    intent.SetClass(this,typeof(LaboratorsExampleActivity));
                    break;
                default:
                    return;//intent.SetClass(this, typeof(FirstPage));
                    break;
            }
            StartActivity(intent);
        }

        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string activityChoser = mLeftDataSet[e.Position];

            var intent = new Intent();
            switch (activityChoser)
            {
                case "Proprietatile unui grup":
                    intent.SetClass(this,typeof(GroupProperty));
                    break;
                case "Prop. prod. 2 grupuri":
                    intent.SetClass(this,typeof(ProduceGroups));
                    break;
                case "Izotopii grupului":
                    intent.SetClass(this,typeof(IzotopGroup));
                    break;
                case "Compunerea substitutiilor":
                    intent.SetClass(this,typeof(CompuneareaSubstitutiilor));
                    break;
                case "Laboratorul 4":
                    intent.SetClass(this, typeof(Laboratorul4Activity));
                    break;
                default:
                    return;//intent.SetClass(this, typeof(FirstPage));
                    break;
            }
            StartActivity(intent);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Android.Resource.Id.Home:
                    //The hamburger icon was clicked which means the drawer toggle will handle the event
                    //all we need to do is ensure the right drawer is closed so the don't overlap
                    mDrawerLayout.CloseDrawer(mRightDrawer);
                    mDrawerToggle.OnOptionsItemSelected(item);
                    return true;

                case Resource.Id.action_refresh:
                    //Refresh
                    return true;

                case Resource.Id.action_help:
                    if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                    {
                        //Right Drawer is already open, close it
                        mDrawerLayout.CloseDrawer(mRightDrawer);
                    }

                    else
                    {
                        //Right Drawer is closed, open it and just in case close left drawer
                        mDrawerLayout.OpenDrawer(mRightDrawer);
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                    }

                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }





        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }

            else
            {
                outState.PutString("DrawerState", "Closed");
            }

            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }





    }
}