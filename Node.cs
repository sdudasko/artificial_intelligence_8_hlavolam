using System;

namespace artificial_intelligence_8_hlavolam
{
    // State: 2d arr
    // Heuristics: Number of nodes PATHS which do not correspond with their position (4+4+3+2+...n(pos)) n = 9
    // We do represent blank as 0 for better visualization

    // Honorable mentions: we might consider adjusting algorithm to not make redundant,
    // possibly infinite loops in case of 2 step same result situation: (1, 2, 3) -> (1, 3, 2) -> (1, 2, 3) deadlock

    // Algorithm:
    // 1. We are creating nodes and putting them into a queue
    public class Node
    {
        public Node parent_node = null; // Parent node
        public string _operator = null; // right, left, ...
        public int depth; // Depth
        public int cost = -1; // Current trace cost
        public int used_operator = -1;

        public Guid uuid;
        public bool handeled = false;

        public int[,] state = null;

        public Node(int[,] state, int used_operator = -1, int depth = 0, Node parent_node = null)
        {
            this.uuid = Guid.NewGuid();
            this.state = state;
            this.used_operator = used_operator; // For the backtrace
            this.depth = depth;
            this.cost = this.calculateCost();
            this.parent_node = parent_node;
        }

        /**
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
                    if ((this.state[i, j] != ( i * 3 ) + j + 1) && this.state[i, j] != 0)
                    {
                        cost += 1;
                    }
                    if ((this.state[i, j] == 1) && (i == 0 && j == 0))
                    {
                        cost--;
                    }
                    if ((this.state[i, j] == 3) && (i == 0 && j == 2))
                    {
                        cost--;
                    }
                    if ((this.state[i, j] == 7) && (i == 2 && j == 0))
                    {
                        cost--;
                    }
                    if ((this.state[i, j] == 5) && (i == 1 && j == 1))
                    {
                        cost++;
                    }
                    cost += this.HowManyPlacesToBeInRightPlace(i, j, this.state[i, j]);
                }
            }
            //Console.WriteLine("Depth: " + this.depth);
            return (cost + this.depth);
        }

        public int HowManyPlacesToBeInRightPlace(int i, int j, int handeledNumber)
        {
            if (handeledNumber == 0)
                return 0;

            int steps = 0;
            int desired_y = (handeledNumber - 1) / 3;
            int desired_x = (handeledNumber - 1) % 3;

            while (j != desired_x)
            {
                if (j < desired_x)
                {
                    j++;
                } else
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
 