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
    public class GroupPropertyRepository
    {
        int[,] t1 = new int[20, 20];
        static int r1, d1, r, d, r2, t, i, j, k, p, d2;
        int[] c, b=new int[100];
        public List<string> allProperty;

        public int[,] matrixProduce = new int[20, 20];

        public int[,] alfaMatrix = new int[20, 20];
        public int[,] betaMatrix = new int[20, 20];
        public int[,] gamaMatrix = new int[20, 20];
        public GroupPropertyRepository(int[,] matricea,int dimensiunea)
        {
            allProperty = new List<string>();
            r = 0; r1 = 0;

            asociativ(matricea, dimensiunea);
            medial(matricea, dimensiunea);
            paramedial(matricea, dimensiunea);
            bicomutativ(matricea, dimensiunea);
            ag_gr(matricea, dimensiunea);
            ga_gr(matricea, dimensiunea);
            ga_gr1(matricea, dimensiunea);
            ad_gr(matricea, dimensiunea);
            da_gr(matricea, dimensiunea);
            hexagonal(matricea, dimensiunea);
            dist_dr(matricea, dimensiunea);
            dist_st(matricea, dimensiunea);
            unitate_dreapta(matricea, dimensiunea, out r);
            unitate_stanga(matricea, dimensiunea, out r1);
            unitate(ref r, ref r1);
            ward(matricea, dimensiunea);
            ward_invers(matricea, dimensiunea);

        }

        public GroupPropertyRepository(int[,] matriceaA,int[,] matriceaB,int dimA,int dimB)
        {
            produs_cartezian(out matrixProduce, matriceaA,matriceaB,dimA,dimB);
             
        }

        public GroupPropertyRepository(int[,] InitialMatrix,int[] alpha,int[] beta,int[] gama,int n)
        {
            ////n- diemsiunea matricei ,,grupului,,,,,, alpha,beta,gama -- substitutiile.
            iteratiaAlfa(ref InitialMatrix, out alfaMatrix, ref alpha, n);
            iteratiaBeta(ref alfaMatrix, out betaMatrix, ref beta, n);
            iteratiaGama(ref betaMatrix, out gamaMatrix, ref gama, n);

        }




        public  void iteratiaAlfa(ref int[,] a, out int[,] b, ref int[] substitutia, int n)////numele iteratiei, array a , array b , dimensiunea n
        {
            b = new int[15, 15];
            int t = 0;
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                {
                    t = substitutia[i];
                    b[i, j] = a[t, j];
                }
            //Console.WriteLine("ITERATIA ALFA");
            //for (int i = 1; i < n + 1; i++)
            //{
            //    for (int j = 1; j < n + 1; j++)
            //    {
            //        Console.Write(b[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }

        public  void iteratiaBeta(ref int[,] a, out int[,] b, ref int[] substitutia, int n)////numele iteratiei, array a , array b , dimensiunea n
        {
            b = new int[15, 15];
            int t = 0;
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                {
                    t = substitutia[j];
                    b[i, j] = a[i, t];
                }
           /// Console.WriteLine("ITERATIA BETA");
            //for (int i = 1; i < n + 1; i++)
            //{
            //    for (int j = 1; j < n + 1; j++)
            //    {
            //        Console.Write(b[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }

        public  void iteratiaGama(ref int[,] a, out int[,] b, ref int[] substitutia, int n)////numele iteratiei, array a(valori) , array b(variabila) , dimensiunea n
        {
            int t = 0;
            b = new int[15, 15];
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                {
                    t = a[i, j];
                    b[i, j] = substitutia[t];
                }
            //Console.WriteLine("ITERATIA GAMA ");
            //for (int i = 1; i < n + 1; i++)
            //{
            //    for (int j = 1; j < n + 1; j++)
            //    {
            //        Console.Write(b[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }



        public  void E(int[,] a, int n)
        {
            int j = 0;
            for (int i = 1; i < n + 1; i++)
            {
                if (a[i, i] == i && nn(a, i, n) != 0 && mm(a, i, n) != 0)
                {
                    Console.WriteLine("=E(" + nn(a, i, n) + "," + mm(a, i, n) + ");"); j++;
                }
            }
            if (j == 0) Console.WriteLine("NU sunt");
        }

        public  int mm(int[,] a, int g, int n)
        {
            int ii, jj, k, x, q;

            for (jj = 1; jj < n + 1; jj++)
            {
                k = 0;
                x = jj;

                do
                {
                    x = a[x, g];
                    k++;
                } while (x != jj && k < n + 1);

                if (k > n + 1) b[jj] = 0;
                else b[jj] = k;
            }

            q = b[2];
            for (jj = 2; jj < n + 1; jj++)
            {
                if (b[jj] > q)
                {
                    q = b[jj];
                    break;
                }

            }
            return q;
        }

        public  int nn(int[,] a, int gg, int n)
        {

            int ii, jj, k, x, q;
            for (jj = 1; jj < n + 1; jj++)
            {
                k = 0;
                x = jj;

                do
                {
                    x = a[gg, x];
                    k++;
                } while (x != jj && k < n + 2);

                if (k > n + 1) c[jj] = 0;
                else c[jj] = k;


            }
            q = c[2];
            for (jj = 2; jj < n + 1; jj++)
            {
                if (c[jj] > q) { q = c[jj]; break; }
            }
            return q;
        }











        public void produs_cartezian(out int[,] masiv, int[,] a, int[,] b, int n, int m)
        {
            int k = 1;
            t1 = new int[20, 20];
            masiv = new int[30, 30];
            int[] c = new int[100];
            int[] f = new int[100];
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < m + 1; j++)
                {
                    f[k] = j;
                    c[k] = i;
                    t1[i, j] = k;
                    k = k + 1;
                }
            for (int i = 1; i < m * n + 1; i++)
                for (int j = 1; j < n * m + 1; j++)
                {
                    masiv[i, j] = t1[a[c[i], c[j]], b[f[i], f[j]]];
                }
        }

        public  void asociativ(int[,] masiv, int n)
        {

            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[j, k];
                        d1 = masiv[i, j];
                        if (masiv[i, d] != masiv[d1, k]) l++;
                    }

                }
            }
            if (l == 0) allProperty.Add("ESTE ASOCIATIV");///Console.WriteLine("ESTE ASOCIATIV");
            else allProperty.Add("NU ESTE ASOCIATIV");

        }



        public  void medial(int[,] masiv, int n)
        {
            int l = 0;

            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        for (int t = 1; t < n + 1; t++)
                        {

                            d = masiv[i, j];
                            r = masiv[k, t];
                            d1 = masiv[i, k];
                            r1 = masiv[j, t];
                            if (masiv[d, r] != masiv[d1, r1])
                                l++;

                        }
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE MEDIAL");
            else allProperty.Add("NU ESTE MEDIAL");

        }

        public  void paramedial(int[,] masiv, int n)
        {
            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        for (int t = 1; t < n + 1; t++)
                        {

                            d = masiv[i, j];
                            r = masiv[k, t];
                            d1 = masiv[t, j];
                            r1 = masiv[k, i];
                            if (masiv[d, r] != masiv[d1, r1]) l++;

                        }
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE PARAMEDIAL");
            else allProperty.Add("NU ESTE PARAMEDIAL");
        }

        public  void bicomutativ(int[,] masiv, int n)
        {

            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        for (int t = 1; t < n + 1; t++)
                        {

                            d = masiv[i, j];
                            r = masiv[k, t];
                            d1 = masiv[t, k];
                            r1 = masiv[j, i];
                            if (masiv[d, r] != masiv[d1, r1]) l++;

                        }
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE BICOMUTATIV");
            else allProperty.Add("NU ESTE BICOMUTATIV");
        }

        public  void ag_gr(int[,] masiv, int n)
        {

            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[i, j];
                        d1 = masiv[k, j];
                        if (masiv[d, k] != masiv[d1, i]) l++;
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE AG GRUPOID");
            else allProperty.Add("NU ESTE AG GRUPOID");
        }

        public  void ga_gr(int[,] masiv, int n)
        {

            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[i, j];
                        d1 = masiv[j, i];
                        if (masiv[d, k] != masiv[k, d1]) l++;
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE GA GRUPOID");
            else allProperty.Add("NU ESTE GA GRUPOID");
        }


        public  void ga_gr1(int[,] masiv, int n)
        {
            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[i, j];
                        d1 = masiv[k, i];
                        if (masiv[d, k] != masiv[d1, j]) l++;
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE GA1 GRUPOID");
            else allProperty.Add("NU ESTE GA1 GRUpoid");
        }

        public  void ad_gr(int[,] masiv, int n)
        {
            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[j, k];
                        d1 = masiv[j, i];
                        if (masiv[i, d] != masiv[k, d1]) l++;
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE AD GRUPOID");
            else allProperty.Add("NU ESTE AD GRUpoid");
        }


        public  void da_gr(int[,] masiv, int n)
        {
            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[j, k];
                        d1 = masiv[i, j];
                        if (masiv[i, d] != masiv[k, d1]) l++;
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE DA GRUPOID");
            else allProperty.Add("NU ESTE DA GRUpoid");
        }



        public  void hexagonal(int[,] masiv, int n)
        {
            int l = 0;
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        for (int t = 1; t < n + 1; t++)
                        {

                            d = masiv[i, j];
                            r = masiv[k, t];
                            d1 = masiv[i, k];
                            r1 = masiv[j, t];
                            r2 = masiv[j, i];
                            if (masiv[i, i] != i || masiv[d, r] != masiv[d1, r1] || masiv[i, r2] != masiv[d, i] && masiv[d, i] != j) l++;

                        }
                    }
                }
            }
            if (l == 0) allProperty.Add("ESTE HEXAGONAL");
            else allProperty.Add("NU ESTE HEXAGONAL");
        }


        public  void dist_dr(int[,] masiv, int n)
        {
            int l = 0;

            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[i, j];
                        d1 = masiv[i, k];
                        r1 = masiv[j, k];
                        if (masiv[d, k] != masiv[d1, r1]) l++;
                    }

            if (l == 0) allProperty.Add("ESTE DISTRIBUTIV DE DREAPTA");
            else allProperty.Add("NU ESTE DISTRIBUTIV DE DREAPTA");

        }

        public  void dist_st(int[,] masiv, int n)
        {
            int l = 0;

            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[i, j];
                        d1 = masiv[k, i];
                        r1 = masiv[k, j];
                        if (masiv[k, d] != masiv[d1, r1]) l++;
                    }

            if (l == 0) allProperty.Add("ESTE DISTRIBUTIV DE STANGA");
            else allProperty.Add("NU ESTE DISTRIBUTIV DE STANGA");

        }


        public  void unitate_dreapta(int[,] masiv, int n, out int r)
        {
            int l;
            int j = 0; r = 0;
            for (int i = 1; i < n + 1; i++)
            {
                l = 0;
                j++;
                if (masiv[j, i] == i)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        if (masiv[k, j] == k) l++;
                        if (l == n) r = j;
                    }
                }
            }
            if (r != 0) allProperty.Add("ESTE UNITATE DREAPTA  " + r);
            else allProperty.Add("NU ESTE UNITATE STANGA");
        }



        public  void unitate_stanga(int[,] masiv, int n, out int r2)
        {
            int l;
            int j = 0; r2 = 0;
            for (int i = 1; i < n + 1; i++)
            {
                l = 0;
                j++;
                if (masiv[i, j] == i)
                {
                    for (int k = 1; k < n + 1; k++)
                    {
                        if (masiv[j, k] == k) l++;
                        if (l == n) r2 = j;
                    }
                }
            }
            if (r2 != 0) allProperty.Add("ESTE UNITATE STANGA  " + r2);
            else allProperty.Add("NU ESTE UNITATE STANGA");
        }


        public  void unitate(ref int r, ref int r2)
        {

            if (r1 == r2 && r > 0) allProperty.Add("Este unitate " + r);
            else allProperty.Add("NU este unitate");

        }

        public  void ward(int[,] masiv, int n)
        {
            int l = 0;
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[i, j];
                        d1 = masiv[i, k];
                        d2 = masiv[j, k];
                        if (d != masiv[d1, d2]) l += 1;
                    }

            if (l == 0) allProperty.Add("ESTE WARD");
            else allProperty.Add("NU ESTE WARD");
        }


        public  void ward_invers(int[,] masiv, int n)
        {
            int l = 0;
            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < n + 1; j++)
                    for (int k = 1; k < n + 1; k++)
                    {
                        d = masiv[i, j];
                        d1 = masiv[k, i];
                        d2 = masiv[k, j];
                        if (d != masiv[d1, d2]) l += 1;
                    }

            if (l == 0) allProperty.Add("ESTE WARD INVERS");
            else allProperty.Add("NU ESTE WARD INVERS");
        }
    }
}