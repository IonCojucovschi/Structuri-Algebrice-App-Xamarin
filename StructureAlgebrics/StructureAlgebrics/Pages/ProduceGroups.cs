using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using StructureAlgebrics.Reposytory;
using Android.Views;

namespace StructureAlgebrics.Pages
{
    [Activity(Label = "Produsul grupurilor", MainLauncher = false, Theme = "@style/MyTheme")]
    public class ProduceGroups : Activity
    {

        private EditText matrixa;
        private EditText matrixb;
        private ListView propertyView;
        private TextView produceView;
        private Button clear;
        private Button showProperty;
        private Thread statusbarThread;
        private ProgressBar progressBar;
        public int progressBarStatus;

        ///\\\\\\\\\\\\\\\\\\\\\
        private List<string> propertiesList;
        int[,] matriceaA;
        int[,] matriceaB;
        int[,] matriceaProdus;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.SetSoftInputMode(SoftInput.AdjustNothing);

            // Create your application here
            SetContentView(Resource.Layout.produce_property);
            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            clear = FindViewById<Button>(Resource.Id.clear_all);
            showProperty = FindViewById<Button>(Resource.Id.show_property);
            propertyView = FindViewById<ListView>(Resource.Id.produce_list_property);
            produceView = FindViewById<TextView>(Resource.Id.produceViewGroup);
            matrixa = FindViewById<EditText>(Resource.Id.grupula);
            matrixb = FindViewById<EditText>(Resource.Id.grupulb);
            progressBar = FindViewById<ProgressBar>(Resource.Id.produce_progressBar);
        }

        private void HandleEvents()
        {
            showProperty.Click += ShowProperty_Click;
            clear.Click += Clear_Click;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            produceView.Text = "";
            propertyView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>());
            statusbarThread.Abort();
        }

        private void ShowProperty_Click(object sender, EventArgs e)
        {
            //hd test 
            //matrixa.Text = "123312231";
            //matrixb.Text = "1221";
           bool progressbarContor = true;
            progressBar.Progress = 0;
            progressBar.Max = 1000;
            progressBarStatus = 0;
            statusbarThread=new Thread(new ThreadStart(delegate {
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

            int dima = GenerateMatrix(out matriceaA,matrixa.Text);
            int dimb = GenerateMatrix(out matriceaB, matrixb.Text);
            if (dima != 0)
            {
                if (dimb != 0)
                {
                    GroupPropertyRepository repo = new GroupPropertyRepository(matriceaA,matriceaB,dima,dimb);
                    matriceaProdus = repo.matrixProduce;
                    repo = new GroupPropertyRepository(matriceaProdus, dima*dimb);
                    propertiesList = repo.allProperty;
                    ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, propertiesList);
                    propertyView.Adapter = ListAdapter;

                    string matrixForm="";
                    for (int i = 1; i < dima * dimb + 1; i++)
                    {
                        for (int j = 1; j < dima * dimb+1; j++)
                        {
                            matrixForm += "  " + matriceaProdus[i, j];
                        }
                        matrixForm += "\n";
                    }


                    produceView.Text = matrixForm;

                }
                else
                {
                    Toast.MakeText(ApplicationContext, "Matricea B trebuie sa fie de forma patrata: N x N", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(ApplicationContext, "Matricea A trebuie sa fie de forma patrata: N x N", ToastLength.Long).Show();
            }
        }

        public int GenerateMatrix(out int[,] matrix1 ,string text)
        {
            matrix1 = new int[20, 20];
            if (text.Length != 0)
            {
                int contor = 0;
                int dim = Convert.ToInt32(Math.Sqrt(text.Length));
                if ((dim * dim) == text.Length)
                {
                    for (int i = 1; i < dim + 1; i++)
                    {
                        for (int j = 1; j < dim + 1; j++)
                        {
                            matrix1[i, j] = Convert.ToInt32(new string(text[contor], 1));
                            contor++;
                        }
                    }
                }
                else { return 0; }
                return dim;
            }
            else { return 0; }
        }
    }
}