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

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.produce_property);
        }

        private void FindViewsWith()
        {
            clear = FindViewById<Button>(Resource.Id.clear_all);
            showProperty = FindViewById<Button>(Resource.Id.show_property);
            propertyView = FindViewById<ListView>(Resource.Id.produce_list_property);


        }

        private void HandleEvents()
        {



        }




    }
}