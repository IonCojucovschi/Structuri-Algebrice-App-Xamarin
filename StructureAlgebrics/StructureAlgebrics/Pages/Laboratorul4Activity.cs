using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using StructureAlgebrics.Reposytory;

namespace StructureAlgebrics.Pages
{
    [Activity(Label = "Laboratorul4Activity",MainLauncher =false,Theme = "@style/MyTheme")]
    public class Laboratorul4Activity : Activity
    {
        private Button legeaI;
        private Button legeaII;
        private Button Clear;
        private EditText initialGr;
        private ListView produceProperty;
        private TextView MatrixView;

        private bool ifIsClicked=false;
        private ProgressBar progressBar;
        private Thread statusbarThread;


        private int[,] matrix;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.laboratorul_4_well);
            // Create your application here
            FindViews();
            HandleEvents();
        }

        private void  FindViews()
        {
            legeaI = FindViewById<Button>(Resource.Id.legeaI);
            legeaII = FindViewById<Button>(Resource.Id.legeaII);
            Clear = FindViewById<Button>(Resource.Id.clear_all);
            initialGr = FindViewById<EditText>(Resource.Id.grupul_gr);
            produceProperty = FindViewById<ListView>(Resource.Id.produce_list_property);
            MatrixView = FindViewById<TextView>(Resource.Id.produceViewGroup);
            progressBar = FindViewById<ProgressBar>(Resource.Id.produce_progressBar);
        }

        private void HandleEvents()
        {
            legeaI.Click += LegeaI_Click;
            legeaII.Click += LegeaII_Click;
            Clear.Click += Clear_Click;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ifIsClicked = false;
            statusbarThread.Abort();
            List<string> propertyes = new List<string>();
            ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, propertyes);
            produceProperty.Adapter = ListAdapter;
            initialGr.Text="";
            MatrixView.Text = "";



        }

        private void LegeaII_Click(object sender, EventArgs e)
        {
            if (ifIsClicked) { }
            else
            {
                ifIsClicked = true;
                InfinitBarAnimation();
            }

            int a = GenerateMatrix(initialGr.Text);
            if (a != 0)
            {
                Labo4Repository repo1 = new Labo4Repository(matrix, a);
                int[,] matrixResult = repo1.produs_cartezian_matrix_legea2;

                GroupPropertyRepository repo2 = new GroupPropertyRepository(matrixResult, a * a);
                List<string> propertyes = repo2.allProperty;
                ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, propertyes);
                produceProperty.Adapter = ListAdapter;
                a = a * a;
                string matrixForm = "";
                for (int i = 1; i < a + 1; i++)
                {
                    for (int j = 1; j < a + 1; j++)
                    {
                        matrixForm += "  " + matrixResult[i, j];
                    }
                    matrixForm += "\n";
                }

                MatrixView.Text = matrixForm;


            }
            else
            {
                Toast.MakeText(ApplicationContext, "Matricea trebuie sa fie de forma patrata: N x N", ToastLength.Long).Show();

            }
        }

        private void LegeaI_Click(object sender, EventArgs e)
        {
            if (ifIsClicked) { }
            else
            {
                ifIsClicked = true;
                InfinitBarAnimation();
            }

            int a = GenerateMatrix(initialGr.Text);
            if (a != 0)
            {
               Labo4Repository repo1 = new Labo4Repository(matrix,a);
               int[,] matrixResult = repo1.produs_cartezian_matrix_legea1;

                GroupPropertyRepository repo2 = new GroupPropertyRepository(matrixResult, a*a);
               List<string> propertyes = repo2.allProperty;
                ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, propertyes);
                produceProperty.Adapter = ListAdapter;
                a = a * a;
                string matrixForm = "";
                for (int i = 1; i < a + 1; i++)
                {
                    for (int j = 1; j < a + 1; j++)
                    {
                        matrixForm += "  " + matrixResult[i, j];
                    }
                    matrixForm += "\n";
                }

                MatrixView.Text = matrixForm;

            }
            else
            {
                Toast.MakeText(ApplicationContext, "Matricea trebuie sa fie de forma patrata: N x N", ToastLength.Long).Show();

            }





        }

        private void InfinitBarAnimation()
        {
            progressBar.Max = 100;
            int progressBarStatus = 0;

            statusbarThread = new Thread(new ThreadStart(delegate {
                while (true)
                {
                    progressBarStatus += 1;
                    if (progressBarStatus == 100) progressBarStatus = 0;
                    progressBar.Progress = progressBarStatus;
                    progressBar.SecondaryProgress = 2 * progressBarStatus;
                    Thread.Sleep(20);//// slep foe 100 ms
                }
            }));
            statusbarThread.Start();
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
                            matrix[i, j] = Convert.ToInt32(new string(text[contor], 1));
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