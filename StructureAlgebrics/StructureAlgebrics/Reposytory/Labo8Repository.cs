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
    class Labo8Repository
    {
        public  string f2;
        public  int[] e, a, b, g, h, r, pr = new int[20];

        public Labo8Repository(int[] ee,int[]aa, int[]bb, int[] gg ,int[]hh, int[]rr)
        {
            e = ee;            a = aa;            b = bb;
            g = gg;            h = hh;            r = rr;
            f2 += "\n";
            prod(e, e); prod(e, a); prod(e, b); prod(e, g); prod(e, h); prod(e, r);
            f2 += "\n";
            prod(a, e); prod(a, a); prod(a, b); prod(a, g); prod(a, h); prod(a, r);
            f2 += "\n";
            prod(b, e); prod(b, a); prod(b, b); prod(b, g); prod(b, h); prod(b, r);
            f2 += "\n";
            prod(g, e); prod(g, a); prod(g, b); prod(g, g); prod(g, h); prod(g, r);
            f2 += "\n";
            prod(h, e); prod(h, a); prod(h, b); prod(h, g); prod(h, h); prod(h, r);
            f2 += "\n";
            prod(r, e); prod(r, a); prod(r, b); prod(r, g); prod(r, h); prod(r, r);
        }


        public  void prod(int[] x, int[] y)
        {
            int p1, p2, p3, p4, p5, p6;

            for (int i = 1; i < 3; i++)
            {
                pr[i] = x[y[i]];
            }

            p1 = 1;
            for (int i = 1; i < 3; i++)
            {
                if (pr[i] == e[i]) { p1 = p1; } else { p1 = 0; }
            }
            if (p1 == 1) { f2 += "e  "; }

            p2 = 1;
            for (int i = 1; i < 3; i++)
            {
                if (pr[i] == a[i]) { } else { p2 = 0; }
            }
            if (p2 == 1) { f2 += "a  "; }

            p3 = 1;
            for (int i = 1; i < 3; i++)
            {
                if (pr[i] == b[i]) { } else { p3 = 0; }
            }
            if (p3 == 1) { f2 += "b  "; }

            p4 = 1;
            for (int i = 1; i < 3; i++)
            {
                if (pr[i] == g[i]) { } else { p4 = 0; }
            }
            if (p4 == 1) { f2 += "g  "; }

            p5 = 1;
            for (int i = 1; i < 3; i++)
            {
                if (pr[i] == h[i]) { } else { p5 = 0; }
            }
            if (p5 == 1) { f2 += "h  "; }

            p6 = 1;
            for (int i = 1; i < 3; i++)
            {
                if (pr[i] == r[i]) { } else { p6 = 0; }
            }
            if (p6 == 1) { f2 += "r  "; }
        }
    }
}