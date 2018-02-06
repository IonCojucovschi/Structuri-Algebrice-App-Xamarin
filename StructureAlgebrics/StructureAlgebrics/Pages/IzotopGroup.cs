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
    [Activity(Label = "IzotopGroup")]
    public class IzotopGroup : Activity
    {
        private EditText grupulA;
        private EditText alfa;
        private EditText beta;
        private EditText gama;
        private TextView iteratiaAlfa;
        private TextView iteratiaBeta;
        private TextView iteratiaGama;
        private ListView grupulAProperty;
        private TextView valE;
        private ListView alfaProperty;
        private ListView betaProperty;
        private ListView gamaProperty;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.izotopii_unui_grupoid_view);
            
            // Create your application here


        }



    }
}