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
using StructureAlgebrics.Reposytory;

namespace StructureAlgebrics.Pages
{
    [Activity(Label = "Proprietatile grupului", MainLauncher = false, Theme = "@style/MyTheme")]
    public class GroupProperty : Activity
    {


        private Button viewProperty;
        private Button clear;
        private ProgressBar progressBar;
        public int progressBarStatus;



        private ListView listPropertiesView;
        private List<string> propertyes;
        private EditText groupElement;

        private Thread statusbarThread;


        /// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        static int[,] matrix;
        private bool progressbarContor = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.SetSoftInputMode(SoftInput.AdjustNothing);
            SetContentView(Resource.Layout.grup_property);
            // Create your application here
            FindViews();
            HandleEvents();
        }

        public void FindViews() {
            viewProperty = FindViewById<Button>(Resource.Id.show_property);
            clear = FindViewById<Button>(Resource.Id.clear_all);
            listPropertiesView = FindViewById<ListView>(Resource.Id.list_property);
            groupElement = FindViewById<EditText>(Resource.Id.grupul);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

        }

        public void HandleEvents()
        {
            viewProperty.Click += ViewProperty_Click;
            clear.Click += Clear_Click;
            progressbarContor = false;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            groupElement.Text = "";
            listPropertiesView.Adapter= new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>());
            statusbarThread.Abort();
        }

        private void ViewProperty_Click(object sender, EventArgs e)
        {
           //// groupElement.Text = "2314413232411423";//autotest
            progressbarContor = true;
            progressBar.Progress = 0;
            progressBar.Max = 1000;

            progressBarStatus = 0;

            statusbarThread = new Thread(new ThreadStart(delegate {
                int i = 0;

                while (i < 100)
                {
                    i++;
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

            }));
            statusbarThread.Start();




            int a = GenerateMatrix(groupElement.Text);
            if (a != 0)
            {
                GroupPropertyRepository repo = new GroupPropertyRepository(matrix, a);
                propertyes = repo.allProperty;
                ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, propertyes);
                listPropertiesView.Adapter = ListAdapter;

            }
            else {
                Toast.MakeText(ApplicationContext, "Matricea trebuie sa fie de forma patrata: N x N", ToastLength.Long).Show();
            }
           
        }

        public int GenerateMatrix(string text)
        {
            if (text.Length != 0)
            {
                matrix = new int[20, 20];
                int contor = 0;
                int dim = Convert.ToInt32(Math.Sqrt(text.Length));
                if ((dim * dim) == text.Length)
                {
                    for (int i = 1; i < dim + 1; i++)
                    {
                        for (int j = 1; j < dim + 1; j++)
                        {
                            matrix[i, j] = Convert.ToInt32(new string(text[contor],1));
                            contor++;
                        }
                        
                    }
                }
                else { return 0; }
                return dim;
            }
            else { return 0; }

            
        }




        protected override void OnStart()
        {
            base.OnStart();
            
        }
    }
}