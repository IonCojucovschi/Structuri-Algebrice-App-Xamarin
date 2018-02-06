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
    [Activity(Label = "Laborators Example", MainLauncher = false, Theme = "@style/MyTheme")]
    public class LaboratorsExampleActivity : Activity
    {
        private List<Labo> laboratorsList;
        private LaboratorsList repos = new LaboratorsList();
        private ListView laboratorsListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.laborator_example_list_view);
            // Create your application here

            FindViewss();

            laboratorsListView.ItemClick += LaboratorsListView_ItemClick;
        }

        private void LaboratorsListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string src = new LaboratorsList().AllLaborators[e.Position].Path;

            var intent = new Intent();
            intent.SetClass(this,typeof(PdfViewer));
            intent.PutExtra("src", src);

            StartActivity(intent);
        }

        private void  FindViewss()
        {
            laboratorsListView = FindViewById<ListView>(Resource.Id.laborators_list_view);

            laboratorsList = repos.AllLaborators;
            List<string> list=new List<string>(); foreach (var item in laboratorsList) { list.Add(item.Name); }
            ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list);
            laboratorsListView.Adapter = ListAdapter;


        }


    }
}