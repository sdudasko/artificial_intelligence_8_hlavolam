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
        string[] _operators = {
            "up", "right", "down", "left",
        };

        Node parent_node = null; // Parent node
        string _operator = null; // right, left, ...
        int depth = -1; // Depth
        int cost = -1; // Current trace cost

        public int[,] state = null;

        public Node(int[,] startingState = null, int[,] satisfiableState = null)
        {
            //this.printMatrix(this.current_state);
            //this.printMatrix(this.satisfiable_state);
        }

        /**
         * https://stackoverflow.com/a/12827010/6525417
         */
        private void printMatrix(int [,] rawNodes)
        {
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

            Console.WriteLine(arrayString);
        }
    }

}
 