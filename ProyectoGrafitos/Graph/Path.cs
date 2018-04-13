using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGrafitos
{
    public class Path
    {
        public Node origin, destination;
        private Path next;
        
        public string value;
        public Path(string value, Node origin, Node destination)
        {
            this.value = value;
            this.origin = origin;
            this.destination = destination;
            this.origin.setSucessor(this);
            this.destination.setPredecessor(this);
        }
        public Path getNext()
        {
            return next;
        }

        public string getValue()
        {
            return value;
        }
        public Node getOrigin()
        {
            return origin;
        }
        public void setOrigin(Node origin)
        {
            this.origin = origin;
        }
        public void setNext(Path next)
        {
            this.next = next;
        }
        public void setValue(string value)
        {
            this.value = value;
        }

        public String toString()
        {
            String info = "Peso: " + value + "\n";
            info += "Origen:\n            " + this.origin.toString();
            info += "Destino:\n           " + this.destination.toString();
            if (this.next != null)
            {
                info += "Siguiente: \n" +
                        "           " + this.next.toString();
            }

            return info;
        }
    }
}
