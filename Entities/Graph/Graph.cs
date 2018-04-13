using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Graph
{
    class Graph
    {
        private Node first;
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

        public void bla(Location a, Location b)
        {

        }
        public void addPath(Location originkey, Location destinationkey, int value)
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
        private Node searchVertice(Location key)
        {
            Node aux = first;
            while (aux != null)
            {
                if (aux.getValue() == key)
                {
                    return aux;
                }
                aux = aux.getNextNode();
            }
            return null;
        }
    }
}
