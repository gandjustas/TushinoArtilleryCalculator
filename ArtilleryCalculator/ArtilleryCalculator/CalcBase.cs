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

    class CalcBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CalcBase(int thousands)
        {
            _thousands = thousands;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void Changed([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
            OnPropertyChanged(nameof(Distance));
            OnPropertyChanged(nameof(Azimuth));
            OnPropertyChanged(nameof(DU));
            OnPropertyChanged(nameof(Angle));
        }

        private double _targetX;
        private double _targetY;
        private double _targetH;
        private double _gunX;
        private double _gunY;
        private double _gunH;
        private bool _invertY;
        private double _tN;
        private int _thousands;

        public double TargetX
        {
            get { return _targetX; }

            set
            {
                if (_targetX != value)
                {
                    _targetX = value;
                    Changed();

                }
            }
        }

        public double TargetY
        {
            get { return _targetY; }
            set
            {
                if (_targetY != value)
                {
                    _targetY = value;
                    Changed();
                }
            }
        }
        public double TargetH
        {
            get { return _targetH; }
            set
            {
                if (_targetH != value)
                {
                    _targetH = value;
                    Changed();
                }
            }
        }
        public double GunX
        {
            get { return _gunX; }
            set
            {
                if (_gunX != value)
                {
                    _gunX = value;
                    Changed();
                }
            }
        }
        public double GunY
        {
            get { return _gunY; }
            set
            {
                if (_gunY != value)
                {
                    _gunY = value;
                    Changed();
                }
            }
        }
        public double GunH
        {
            get { return _gunH; }
            set
            {
                if (_gunH != value)
                {
                    _gunH = value;
                    Changed();
                }
            }
        }

        public bool InvertY
        {
            get { return _invertY; }
            set
            {
                if (_invertY != value)
                {
                    _invertY = value;
                    Changed();
                }
            }
        }

        public double TN
        {
            get { return _tN; }
            set
            {
                if (_tN != value)
                {
                    _tN = value;
                    Changed();
                }
            }
        }

        public double Distance
        {
            get
            {
                var x = TargetX - GunX;
                var y = TargetY - GunY;
                if (_invertY) y = -y;

                return Math.Sqrt(x * x + y * y) * 100;
            }
        }
        public double Azimuth
        {
            get
            {
                var x = TargetX - GunX;
                var y = TargetY - GunY;
                if (_invertY) y = -y;

                var atan = Math.Atan2(y, x);
                var d = Math.PI / 2 - atan;
                if (d < 0) d += d + 2 * Math.PI;
                return d * 180 / Math.PI;
            }
        }
        public double DU
        {
            get
            {
                var x = TargetX - GunX;
                var y = TargetY - GunY;
                if (_invertY) y = -y;

                var atan = Math.Atan2(y, x);
                var d = Math.PI / 2 - atan;
                var a = d * _thousands / 2 / Math.PI;
                if (a < 0) a += _thousands;
                return a;
            }
        }


        public double Angle
        {
            get
            {
                var a = _thousands / 2 - TN + DU;
                if (a < 0) a += _thousands;
                return a;
            }
        }

        public string Name { get; set; }

        public ArtSettings[] Settings { get; protected set; }
    }


    public class ArtSettings : INotifyPropertyChanged
    {
        private double elev;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name { get; set; }
        public double Elev
        {
            get
            {
                return elev;
            }
            set
            {
                if (elev != value)
                {
                    elev = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
