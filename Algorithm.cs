using System;
namespace artificial_intelligence_8_hlavolam
{
    public class Algorithm
    {
        Node[] state_tree = new Node[] { };
        int width = 3;
        int height = 3;

        int[,] starting_state = new int[3, 3] { // Current state
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
        };

        int[,] satisfiable_state = new int[3, 3] { // Current state
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 0, 8 },
        };

        public void Start()
        {
            Queue control_queue = new Queue();

            Node starting_node = new Node(this.starting_state);

            control_queue.append(starting_node);

            // Handle starting node
            int[,] right_state = this.AfterPerformingRightState(starting_node.state);

        }

        public void Run()
        {
            // Select the best node
        }

        public int[,] AfterPerformingRightState(int[,] state)
        {
            int[,] requested_state = state.Clone() as int[,];

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if(requested_state[i, j] == 0)
                    {
                        if (j != this.width - 1)
                        {
                            return null; // We cannot perform such an operation. 0 (Blank) is not on the very right of the matrix.
                        } else {
                            requested_state[i, j] = requested_state[i, j - 1]; // Here we can perform such an operation so we are returning new state
                            requested_state[i, j - 1] = 0;
                            break; // We have already successfully satisfied the purpose of this cycle so there is no need to look for other blank
                        }
                    }
                }
            }

            return requested_state;

        }
    }
}
