using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace ProyectoGrafitos
{
    public class Graph
    {
        public Node first;
        public Graph()
        {
        }
        public void addVertice(Location value)
        {
            if (this.first == null)
            {
                this.first = new Node(value);
            }
            else
            {
                Node aux = first;
                while (aux != null)
                {
                    if (aux.getNextNode() == null)
                    {
                        aux.setNextNode(new Node(value));
                        break;
                    }
                    aux = aux.getNextNode();
                }
            }
        }

        public void removePath(string key)
        {
            var aux = first;
            while (aux != null)
            {
                var path = aux.getSucessor();
                if (path != null)
                {
                    if (path.value == key)
                    {
                        aux.sucessor = null;
                        aux.setSucessor(path.getNext());
                        break;
                    }
                    var pathToDel = path.getNext();
                    while (pathToDel != null)
                    {
                        if (pathToDel.value == key)
                        {
                            path.setNext(pathToDel.getNext());
                        }

                        path = pathToDel;
                        pathToDel = pathToDel.getNext();
                    }
                  
                }
                aux = aux.getNextNode();
            }
        }
        public GMapOverlay setRoutes(GMapOverlay Routes)
        {
            var aux = this.first;
            while (aux != null)
            {
                var path = aux.getSucessor();
                while (path!=null)
                {
                    var direccion = this.addRoute(path.getOrigin().getValue().possition, path.destination.getValue().possition);
                    GMapRoute route = new GMapRoute(direccion.Route, "Route");
                    Routes.Routes.Add(route);
                    path = path.getNext();
                }

                aux = aux.getNextNode();
            }
            return Routes;
        }
        public GDirections addRoute(PointLatLng a, PointLatLng b)
        {
            
            GDirections direccion;
            var routes = GMapProviders.GoogleMap.GetDirections(out direccion, a,
                b, false, false, false, false, false);
            GMapRoute route = new GMapRoute(direccion.Route, "Route");
            return direccion;
        }
        public void addPath(string originkey, string destinationkey, string value)
        {
            Node origin = this.searchVertice(originkey);
            Node destination = this.searchVertice(destinationkey);
            Path newPath = new Path(value, origin, destination);
        }
        public void printGraph()
        {
            Node aux = first;
            while (aux != null)
            {
                if (aux.getSucessor() != null)
                {
                    Console.WriteLine(aux.printPaths());
                }
                aux = aux.getNextNode();
            }
        }

        public List<List<Path>> getAllPaths()
        {
            var list = new List<List<Path>>();
            var aux = this.first;
            while (aux != null)
            {
                list.Add(getPathsFromVertice(aux));
                aux = aux.getNextNode();
            }
            return list;
        }

        public void RemovePath(String key)
        {

        }
        public List<Path> getPathsFromVertice(Node verice)
        {
            var list = new List<Path>();
            var aux = verice.getSucessor();
            while (aux != null)
            {
                list.Add(aux);
                aux = aux.getNext();
            }
            return list;
        }
        public Node searchVertice(string key)
        {
            Node aux = first;
            while (aux != null)
            {
                if (aux.getValue().name == key)
                {
                    return aux;
                }
                aux = aux.getNextNode();
            }
            return null;
        }
    }
}
