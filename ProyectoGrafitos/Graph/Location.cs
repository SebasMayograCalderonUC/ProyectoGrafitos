using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace ProyectoGrafitos
{
    public class Location
    {
        public Location(string name, double lattitude, double logitude )
        {
            this.lattitude = lattitude;
            this.longitude = logitude;
            this.possition= new PointLatLng(this.lattitude, this.longitude);
            this.name = name;
        }
        public PointLatLng possition { get; set; }
        public string name { get; set; }
        private double lattitude { get; set; }
        private double longitude { get; set; }
       
    }
}
