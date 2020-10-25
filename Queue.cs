using System;
using System.Collections.Generic;
using System.Linq;

namespace artificial_intelligence_8_hlavolam
{
    public class Queue
    {
        List<Node> nodes = new List<Node>();

        public void append(Node node)
        {
            Algorithm.nodes_created++;
            this.nodes.Add(node);
        }
        public void remove(Node node)
        {
            foreach (Node elem in this.nodes)
            {
                if (elem.uuid == node.uuid)
                {
                    node.handeled = true;
                }
            }
        }

        public Node getFromHeap()
        {
            if (this.getUnhandeledNodes().Count == 0)
                return null;

            Node compared = this.getUnhandeledNodes()[0];


            foreach (Node elem in this.getUnhandeledNodes())
            {
                if (elem.cost < compared.cost)
                {
                    compared = elem;
                }
            }

            return compared;


        }

        public List<Node> getUnhandeledNodes()
        {

            List<Node> unhandeledNodes = new List<Node>();

            foreach(Node node in this.nodes)
            {
                if (!node.handeled)
                {
                    unhandeledNodes.Add(node);
                }
                
            }

            return unhandeledNodes;
        }

        public void printAllNodesUUIDs(bool handeled = true)
        {
            if (handeled)
            {
                foreach (Node elem in this.getUnhandeledNodes())
                {
                    Console.WriteLine(elem.uuid + " -> " + elem.cost);
                }
            }

        }

        public bool check_in_old_states_for_state(Node needle) // 2 nodes' states are equal
        {
            bool equal;
            foreach (Node node in this.nodes)
            {
                if (node.handeled)
                {
                    equal = true;
                    for (int i = 0; i < Algorithm.height; i++)
                    {
                        for (int j = 0; j < Algorithm.width; j++)
                        {
                            if (needle.state[i, j] != node.state[i, j])
                            {
                                equal = false;
                            }
                        }
                    }
                    if (equal)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
