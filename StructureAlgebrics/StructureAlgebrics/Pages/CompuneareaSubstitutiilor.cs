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
    [Activity(Label = "CompuneareaSubstitutiilor", MainLauncher = false, Theme = "@style/MyTheme")]
    public class CompuneareaSubstitutiilor : Activity
    {
        private EditText sube;
        private EditText suba;
        private EditText subb;
        private EditText subg;
        private EditText subh;
        private EditText subr;
        private TextView rezult;

        private Button viewButton;
        private Button clearButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.labo_8_view);
            // Create your application here
            FindViews();
            HandleEvents();
        }
        public void FindViews()
        {
            suba = FindViewById<EditText>(Resource.Id.suba);
            subg = FindViewById<EditText>(Resource.Id.subg);
            sube = FindViewById<EditText>(Resource.Id.sube);
            subb = FindViewById<EditText>(Resource.Id.subb);
            subh = FindViewById<EditText>(Resource.Id.subh);
            subr = FindViewById<EditText>(Resource.Id.subr);
            rezult = FindViewById<TextView>(Resource.Id.content_f2);
            viewButton = FindViewById<Button>(Resource.Id.show_property);
            clearButton = FindViewById<Button>(Resource.Id.clear_all);
        }
        public void HandleEvents()
        {
            viewButton.Click += ViewButton_Click;
            clearButton.Click += ClearButton_Click;

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            suba.Text = "";
            subb.Text = "";
            subg.Text = "";
            subh.Text = "";
            subr.Text = "";
            sube.Text = "";
            rezult.Text = "";
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
                int[] vala, valb, vale, valh, valr, valg;
            int a, b, ee, g, h, r;
            a = GenerateSubstitution(suba.Text,out vala);
            b = GenerateSubstitution(subb.Text, out valb);
            ee = GenerateSubstitution(sube.Text, out vale);
            g = GenerateSubstitution(subg.Text, out valg);
            h = GenerateSubstitution(subh.Text, out valh);
            r = GenerateSubstitution(subr.Text, out valr);
            if (a == 3 && a == b && a == h && a == ee && a == g && a == r)
            {
                var repo = new Labo8Repository(vale,vala,valb,valg, valh, valr);
                string f2 = repo.f2;
                rezult.Text = f2;
            }
            else
            {
                Toast.MakeText(ApplicationContext, "   Dimansiunile substitutiilor nu corespund. Dim 3.", ToastLength.Long).Show();

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
    }
}