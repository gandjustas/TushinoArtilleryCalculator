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
    class TableCalc : CalcBase
    {
        ArtTable[] table;

        public TableCalc(int thousands, params ArtTable[] table) : base(thousands)
        {
            this.table = table;
            this.Settings = table.Select(t => new ArtSettings { Name = t.Name }).ToArray();
        }


        protected override void Changed([CallerMemberName] string propertyName = "")
        {
            base.Changed(propertyName);
            for (int i = 0; i < this.table.Length; i++)
            {
                this.Settings[i].Elev = Elev(i);
            }
        }




        private double Elev(int index)
        {
            var t = table[index].Table;
            var points = t.Zip(t.Skip(1), Tuple.Create).SkipWhile(p => p.Item2[0] < this.Distance).FirstOrDefault();
            if (points == null) return -1;
            var low = points.Item1;
            var high = points.Item2;
            var k = (this.Distance - low[0]) / (high[0] - low[0]);
            var dh = (this.TargetH - this.GunH) / 100;

            return low[1] * (1 - k) + high[1] * k + (low[2] * (1 - k) + high[3] * k) * dh;
        }
    }

    public sealed class ArtTable
    {
        public ArtTable(string name, double[][] table)
        {
            Name = name;
            Table = table;
        }
        public string Name { get; private set; }
        public double[][] Table { get; private set; }
    }
}
