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
    class Labo4Repository
    {
        public int[,] tabel, opus, gr, gr2=new int[25,25];
        public int[,] produs_cartezian_matrix_legea1, produs_cartezian_matrix_legea2;
        public Labo4Repository(int[,] _gr,int n)
        {
            opus = new int[20, 20];
            tabel = new int[20, 20];

            Topus(out gr,ref _gr,n);
            produs_cartezian_special_legea1(out gr2, opus, gr, n);
            produs_cartezian_matrix_legea1 = gr2;
            produs_cartezian_special_legea1(out gr2, opus, gr, n);
            produs_cartezian_matrix_legea2 = gr2;

        }

        public  void Topus(out int[,] MayBeGR,ref int[,] grInitialized, int n)
        {
            int element;
            MayBeGR = new int[25, 25];
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                {
                    element = grInitialized[i, j];

                    MayBeGR[i, j] = element;
                    if (element == 1) { opus[i, 1] = j; }
                }
            int k = 1;
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                {
                    tabel[i, j] = k;
                    k++;
                }
        }

        public  void produs_cartezian_special_legea1(out int[,] masiv, int[,] opus, int[,] gr, int n)
        {
            int k1, k2;
            k1 = 0;
            masiv = new int[30, 30];
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                {
                    k1 = k1 + 1; k2 = 0;
                    for (int i2 = 1; i2 < n + 1; i2++)
                        for (int j2 = 1; j2 < n + 1; j2++)
                        {
                            k2 = k2 + 1;
                            gr2[k1, k2] = tabel[gr[gr[i, j], opus[j2, 1]], gr[gr[i2, j2], opus[i, 1]]];
                        }
                }
        }
        public void produs_cartezian_special_legea2(out int[,] masiv, int[,] opus, int[,] gr, int n)
        {
            int k1, k2;
            k1 = 0;
            masiv = new int[30, 30];
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                {
                    k1 = k1 + 1; k2 = 0;
                    for (int i2 = 1; i2 < n + 1; i2++)
                        for (int j2 = 1; j2 < n + 1; j2++)
                        {
                            k2 = k2 + 1;
                            gr2[k1, k2] = tabel[gr[gr[i, j], opus[j2, 1]], gr[gr[i2, j2], opus[j, 1]]];//// legea a doua 
                        }
                }
        }
    }
}