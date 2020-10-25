using System;

namespace artificial_intelligence_8_hlavolam
{
    // State: 2d arr
    // Heuristics: Number of nodes PATHS which do not correspond with their position (4+4+3+2+...n(pos)) n = 9, depth
    // We do represent blank as 0 for better visualization

    // Algorithm:
    // 1. We are creating nodes and putting them into a queue
    public class Node
    {
        public Node parent_node = null; // Parent node
        public int depth; // Depth
        public int cost = -1; // Current trace cost
        public int used_operator = -1;

        public Guid uuid;
        public bool handeled = false;
        public bool from_starting = true;

        public int[,] state = null;

        public Node(int[,] state, int used_operator = -1, int depth = 0, Node parent_node = null, bool from_starting = true)
        {
            this.uuid = Guid.NewGuid();
            this.state = state;
            this.used_operator = used_operator; // For the backtrace

            this.depth = depth;
            this.cost = this.calculateCost();
            this.parent_node = parent_node;
            this.from_starting = from_starting;
        }

        /**
         * Just for testing purposes
         * Code used from:
         * https://stackoverflow.com/a/12827010/6525417
         */
        public void printMatrix(int [,] rawNodes = null)
        {
            if (rawNodes == null)
                rawNodes = this.state;

            int rowLength = rawNodes.GetLength(0);
            int colLength = rawNodes.GetLength(1);
            string arrayString = "";
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    arrayString += string.Format("{0} ", rawNodes[i, j]);
                }
                arrayString += System.Environment.NewLine + System.Environment.NewLine;
            }

            Console.Write(arrayString);
        }

        private int calculateCost()
        {
            int cost = 0;

            for (int i = 0; i < Algorithm.height; i++)
            {
                for (int j = 0; j < Algorithm.width; j++)
                {
                    cost += this.HowManyPlacesToBeInRightPlace(i, j, this.state[i, j]);
                }
            }
            return cost + depth; // We use depth as heuristics as well
        }

        /*
         * We use destination trace length as heuristics
         */
        public int HowManyPlacesToBeInRightPlace(int i, int j, int handeledNumber)
        {
            if (handeledNumber == 0)
                return 0;

            int desired_x = 0;
            int desired_y = 0;

            if (this.from_starting)
            {
                desired_x = Algorithm.satisfiable_state_order[handeledNumber]; // We get the final position of current handeled Number
                desired_x = desired_x % Algorithm.width;

                desired_y = Algorithm.satisfiable_state_order[handeledNumber]; // We get the final position of current handeled Number
                desired_y = desired_y / Algorithm.height;

                // There are directions where we desire to have given number in the end of algorithm
            } else
            {
                desired_x = Algorithm.starting_state_order[handeledNumber]; // We get the final position of current handeled Number
                desired_x = desired_x % Algorithm.width;

                desired_y = Algorithm.starting_state_order[handeledNumber]; // We get the final position of current handeled Number
                desired_y = desired_y / Algorithm.height;
            }

            int steps = 0;


            while (j != desired_x)
            {
                if (j < desired_x)
                {
                    j++;
                }
                else
                {
                    j--;
                }
                steps++;
            }
            while (i != desired_y)
            {
                if (i < desired_y)
                {
                    i++;
                }
                else
                {
                    i--;
                }
                steps++;
            }

            return steps;
        }
    }

}
 