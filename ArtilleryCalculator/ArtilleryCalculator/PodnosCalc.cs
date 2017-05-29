using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ArtilleryCalculator
{
    class PodnosCalc : CalcBase
    {
        public PodnosCalc() : base(6000)
        {
            this.Name = "2Б14";
            this.Settings = new ArtSettings[4];
            this.Settings[0] = new ArtSettings { Name = "Заряд 1\n150-1450" };
            this.Settings[1] = new ArtSettings { Name = "Заряд 2\n200-2400" };
            this.Settings[2] = new ArtSettings { Name = "Заряд 3\n250-3100" };
            this.Settings[3] = new ArtSettings { Name = "Заряд 4\n300-3900" };
        }

        private static double[][] coeffsE1 = new double[][] {
            new double[] {156.44130153, +2.88213657 } ,
            new double[] {214.62678956,0.85156652} ,
            new double[] {216.29275103,0.63832564},
            new double[] {216.40510502,0.51610179} ,
        };
        private static double[][] coeffsE2 = new double[][] {
            new double[] { 1, +0.00722151, -0.00000314665384 } ,
            new double[] {1,0.00189128,-0.000000531209967},
            new double[] {1,0.00140315,-0.000000305622338},
            new double[] {1,0.0011421,-0.000000198425331},
        };
        private static double[][] coeffsD1 = new double[][] {
            new double[] {-4.87132309,0.04649671} ,
            new double[] {-4.50192363,0.02522599},
            new double[] {-2.77913327,0.01350203} ,
            new double[] {-13.78073544,0.04749195},
        };
        private static double[][] coeffsD2 = new double[][] {
            new double[]  { 1, +0,00611335, -0.00000435302904} ,
            new double[] {1,0.00600352,-0.00000254028235},
            new double[] {1,0.00409855,-0.00000135847892},
            new double[] {1,0.0214336,-0.00000536924875},
        };



        protected override void Changed([CallerMemberName] string propertyName = "")
        {
            base.Changed(propertyName);
            for (int i = 0; i < Settings.Length; i++)
            {
                Settings[i].Elev = E1(i, Distance) / E2(i, Distance) + D1(i, Distance) / D2(i, Distance) * (GunH - TargetH) / 100;

            }
        }


        private double E1(int index, double value)
        {
            return V(coeffsE1[index], value);
        }
        private double E2(int index, double value)
        {
            return V(coeffsE2[index], value);
        }

        private double D1(int index, double value)
        {
            return V(coeffsD1[index], value);
        }
        private double D2(int index, double value)
        {
            return V(coeffsD2[index], value);
        }

        private double V(double[] p, double value)
        {
            var x = 1.0;
            var r = 0.0;
            for (int i = 0; i < p.Length; i++)
            {
                r += p[i] * x;
                x *= value;
            }
            return r;
        }

    }
}
