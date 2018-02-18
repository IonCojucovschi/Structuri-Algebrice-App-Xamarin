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

namespace StructureAlgebrics.Reposytory
{
    public class LaboratorsList
    {
        public List<Labo> AllLaborators;

        public LaboratorsList()
        {
            AllLaborators = new List<Labo>();
            InitializaListLaborators();
        }
        private void InitializaListLaborators ()
        {
            AllLaborators.Add(new Labo { Name="Laboratoarele numarul 1-2",Path= "Laborator1_2.pdf" });
            AllLaborators.Add(new Labo { Name="Laboratorul numarul 3",Path= "Laborator3.pdf" });
            AllLaborators.Add(new Labo { Name="Laboratorul numarul 4",Path= "Laborator4.pdf" });
            AllLaborators.Add(new Labo { Name="Laboratorul numarul 5",Path= "Laborator5.pdf" });
            AllLaborators.Add(new Labo { Name="Laboratorul numarul 6",Path= "Laborator6.pdf" });
            AllLaborators.Add(new Labo { Name="Laboratorul numarul 7",Path= "Laborator7.pdf" });
            AllLaborators.Add(new Labo { Name="Laboratorul numarul 8",Path= "Laborator8.pdf" });

        }

    }

    public class Labo
    {
        public string Name { get; set; }
        public string Path { get; set; }

    }
}