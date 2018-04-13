using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Graph
{
    class Location
    {
        Location(string name, double lattitude, double logitude )
        {
            this.lattitude = lattitude;
            this.longitude = logitude;
            this.name = name;
        }
        private string name { get; set; }
        private double lattitude { get; set; }
        private double longitude { get; set; }
    }
}
