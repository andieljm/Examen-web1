using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Domain.Generics
{
    public class Node<T>
        where T : class
    {
        public Node(T value)
        {
            Value = value;
            Nodes = new List<Node<T>>();
        }

        public T Value { get; set; }

        public void ChangeValue(T value)
        {
            Value = value;
        }

        public List<Node<T>> Nodes { get; private set; }
    }
}
