using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;

using Android.Widget;

namespace StructureAlgebrics.Pages
{
    [Activity(Label = "Structuri Algebrice", MainLauncher = true, Theme = "@style/MyTheme")]
    public class GroupProperty : Activity
    {
        private Button viewProperty;
        private Button clear;
        private ProgressBar progressBar;
        private ListView listProperties;
        private EditText groupElement;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.grup_property);
            // Create your application here
            FindViews();
            HandleEvents();
        }

        public void FindViews() {
            viewProperty = FindViewById<Button>(Resource.Id.show_property);
            clear = FindViewById<Button>(Resource.Id.clear_all);
            listProperties = FindViewById<ListView>(Resource.Id.list_property);
            groupElement = FindViewById<EditText>(Resource.Id.grupul);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

        }

        public void HandleEvents()
        {
            viewProperty.Click += ViewProperty_Click;



        }
        public int progressBarStatus;

        private void ViewProperty_Click(object sender, EventArgs e)
        {

            progressBar.Progress = 0;
            progressBar.Max = 1000;

            progressBarStatus = 0;

            ///run thread for increase progress bar
            new Thread(new ThreadStart(delegate {

                while (true)
                {

                    while (progressBarStatus < 1000)
                    {
                        progressBarStatus += 1;
                        if (progressBarStatus < 500)
                        {
                            progressBar.Progress = progressBarStatus;
                            progressBar.SecondaryProgress = 2 * progressBarStatus;
                        }
                        else
                        {
                            progressBar.SecondaryProgress += 2;
                            progressBar.Progress = progressBarStatus + 1;
                        }
                        Thread.Sleep(1);//// slep foe 100 ms
                    }
                    progressBarStatus = 0;
                    progressBar.Progress = 0;
                    progressBar.SecondaryProgress = 0;
                }
                RunOnUiThread(() => { });

            })).Start();



        }

        protected override void OnStart()
        {
            base.OnStart();
            
        }
    }
}