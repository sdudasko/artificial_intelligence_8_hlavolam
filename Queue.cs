using System;
using System.Collections.Generic;

namespace artificial_intelligence_8_hlavolam
{
    public class Queue
    {
        Node[] nodes = new Node[] { };

        public void append()
        {
            this.nodes[this.nodes.Length] = new Node();
        }

        public Node getFromHeap()
        {
            if (this.nodes.Length == 0)
                return null;

            return this.nodes[this.nodes.Length - 1];
        }
    }
}
