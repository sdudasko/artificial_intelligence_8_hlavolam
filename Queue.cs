using System;
using System.Collections.Generic;

namespace artificial_intelligence_8_hlavolam
{
    public class Queue
    {
        List<Node> nodes = new List<Node>();

        public void append(Node node)
        {
            this.nodes.Add(node);
        }

        public Node getFromHeap()
        {
            if (this.nodes.Count == 0)
                return null;

            return this.nodes[this.nodes.Count - 1];
        }
    }
}
