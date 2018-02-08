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
using StructureAlgebrics.Reposytory;

namespace StructureAlgebrics.Pages
{
    [Activity(Label = "Izotopi", MainLauncher = false, Theme = "@style/MyTheme")]
    public class IzotopGroup : Activity
    {
        private EditText grupulA;
        private EditText alfa;
        private EditText beta;
        private EditText gama;
        private TextView iteratiaAlfa;
        private TextView iteratiaBeta;
        private TextView iteratiaGama;
        private TextView valEa;
        private TextView valEg;
        private ListView gamaProperty;
        private ListView grupulAProperty;


        private Button viewButton;
        private Button clearButton;

        static int[,] matrix;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.izotopii_unui_grupoid_view);
            Window.SetSoftInputMode(SoftInput.AdjustNothing);

            // Create your application here
            FindViewss();
            HandleEvents();

        }
        public void FindViewss()
        {
            grupulA = FindViewById<EditText>(Resource.Id.grupul_a);
            alfa = FindViewById<EditText>(Resource.Id.alfa);
            beta = FindViewById<EditText>(Resource.Id.beta);
            gama = FindViewById<EditText>(Resource.Id.gama);

            iteratiaAlfa = FindViewById<TextView>(Resource.Id.iteratia_alfa);
            iteratiaBeta = FindViewById<TextView>(Resource.Id.iteratia_beta);
            iteratiaGama = FindViewById<TextView>(Resource.Id.iteratia_gama);

            grupulAProperty = FindViewById<ListView>(Resource.Id.grupul_a_property);
            gamaProperty = FindViewById<ListView>(Resource.Id.gama_property);

            valEa = FindViewById<TextView>(Resource.Id.values_Ea);
            valEg = FindViewById<TextView>(Resource.Id.values_Eg);

            viewButton = FindViewById<Button>(Resource.Id.show_property_i);
            clearButton = FindViewById<Button>(Resource.Id.clear_all_i);

        }

        private void HandleEvents()
        {
            viewButton.Click += ViewButton_Click;

        }

        private void ViewButton_Click(object sender, EventArgs e)
        {

            int dim = GenerateMatrix(grupulA.Text);
            int[] alphasubsit;
            int dimAlpha = GenerateSubstitution(alfa.Text, out alphasubsit);
            int[] betasubstit;
            int dimBeta = GenerateSubstitution(beta.Text, out betasubstit);
            int[] gamasubstit;
            int dimGama = GenerateSubstitution(gama.Text, out gamasubstit);
            int dimGroup = GenerateMatrix(grupulA.Text);

            if (dimAlpha == dimBeta & dimAlpha == dimGama & dimAlpha == dimGroup)
            {
                GroupPropertyRepository repo = new GroupPropertyRepository(matrix, alphasubsit, betasubstit, gamasubstit, dimAlpha);

                int[,] matriceaAlfa = new int[20, 20];
                int[,] matriceaBeta = new int[20, 20];
                int[,] matriceaGama = new int[20, 20];
                matriceaAlfa = repo.alfaMatrix;
                matriceaBeta = repo.betaMatrix;
                matriceaGama = repo.gamaMatrix;

                string matrixFormAlpha = "";
                for (int i = 1; i < dimAlpha + 1; i++)
                {
                    for (int j = 1; j < dimAlpha + 1; j++)
                    {
                        matrixFormAlpha += "  " + matriceaAlfa[i, j];
                    }
                    matrixFormAlpha += "\n";
                }
                string matrixFormBeta = "";
                for (int i = 1; i < dimAlpha + 1; i++)
                {
                    for (int j = 1; j < dimAlpha + 1; j++)
                    {
                        matrixFormBeta += "  " + matriceaBeta[i, j];
                    }
                    matrixFormBeta += "\n";
                }

                string matrixFormGama = "";
                for (int i = 1; i < dimAlpha + 1; i++)
                {
                    for (int j = 1; j < dimAlpha + 1; j++)
                    {
                        matrixFormGama += "  " + matriceaGama[i, j];
                    }
                    matrixFormGama += "\n";
                }

                iteratiaAlfa.Text = matrixFormAlpha;
                iteratiaBeta.Text = matrixFormBeta;
                iteratiaGama.Text = matrixFormGama;



                List<string> propGroupA = new GroupPropertyRepository(matrix, dimGroup).allProperty;
                List<string> propGrGama = new GroupPropertyRepository(matriceaGama, dimGroup).allProperty;

                ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, propGroupA);
                grupulAProperty.Adapter = ListAdapter;
                ArrayAdapter ListAdapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, propGrGama);
                gamaProperty.Adapter = ListAdapter;

                string valuesEA = repo.E(matrix,dimGroup);
                string valuesEG = repo.E(matriceaGama, dimGroup);
                valEa.Text = valuesEA;
                valEg.Text = valuesEG;


            }
            else
            {

                Toast.MakeText(ApplicationContext, "Dimansiunile grupului sau a substitutiilor nu corespund.", ToastLength.Long).Show();

            }


        }



        public int GenerateSubstitution(string text, out int[] substitution)
        {
            substitution = new int[20];
            int dim = 0;
            if (text.Length != 0)
            {
                for (int i = 1; i < text.Length + 1; i++)
                {
                    substitution[i] = Convert.ToInt32(new string(text[i - 1], 1));
                }
                return text.Length;
            }
            else return dim;


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