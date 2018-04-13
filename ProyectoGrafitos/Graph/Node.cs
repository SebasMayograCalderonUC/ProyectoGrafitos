using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGrafitos
{
    public class Node
    {
        public Path predecessor, sucessor;
        public Node nextNode;
        public Location value;

        public Node(Location value)
        {
            this.value = value;
        }
        public void setPredecessor(Path predecessor)
        {
            this.predecessor = predecessor;
        }

        public void setSucessor(Path sucessor)
        {
            if (this.getSucessor() == null)
            {
                this.sucessor = sucessor;
            }
            else
            {
                Path aux = this.getSucessor();
                while (aux != null)
                {
                    if (aux.getNext() == null)
                    {
                        aux.setNext(sucessor);
                        break;
                    }
                    aux = aux.getNext();
                }
            }
        }

        public void setNextNode(Node nextNode)
        {
            this.nextNode = nextNode;
        }

        public void setValue(Location value)
        {
            this.value = value;
        }

        public Path getPredecessor()
        {

            return predecessor;
        }

        public Path getSucessor()
        {
            return sucessor;
        }

        public Node getNextNode()
        {
            return nextNode;
        }

        public Location getValue()
        {
            return value;
        }
        public String printPaths()
        {
            return this.sucessor.ToString();
        }
 
        public String toString()
        {
            String info = "-------------Vertice-------------------\n";
            info += "Value: " + this.value + "\n";
            return info;
        }
    }
}
