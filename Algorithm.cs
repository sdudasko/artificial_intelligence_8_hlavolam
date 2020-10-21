using System;
namespace artificial_intelligence_8_hlavolam
{
    public class Algorithm
    {
        Node[] state_tree = new Node[] { };
        int width = 3;
        int height = 3;

        string[] _operators = { // We'll translate later, we don't want to map by strings so we use indexes in the project
            "up", // 0.
            "right", // 1.
            "down", // 2.
            "left", // 3.
        };

        int[,] starting_state = new int[3, 3] { // Current state
            { 1, 2, 3 },
            { 4, 0, 6 },
            { 7, 5, 8 },
        };

        int[,] satisfiable_state = new int[3, 3] { // Current state
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 0, 8 },
        };

        public void Handle()
        {
            Queue control_queue = new Queue();

            Node starting_node = new Node(this.starting_state);

            control_queue.append(starting_node);

            // Handle starting node
            int[,] up_state = this.AfterPerformingUpState(starting_node.state);
            int[,] right_state = this.AfterPerformingRightState(starting_node.state);
            int[,] down_state = this.AfterPerformingDownState(starting_node.state);
            int[,] left_state = this.AfterPerformingLeftState(starting_node.state);

            Console.WriteLine("666:");
            if (up_state != null) // There is an option to perform up, so we are creating new node for the up operation
            {
                Node node_after_up_operation = new Node(up_state, 1);
                control_queue.append(node_after_up_operation);
                Console.WriteLine("Up:");
                node_after_up_operation.printMatrix();
            }
            if (right_state != null) // There is an option to perform right, so we are creating new node for the right operation
            {
                Node node_after_right_operation = new Node(right_state, 1);
                control_queue.append(node_after_right_operation);
                Console.WriteLine("Right:");
                node_after_right_operation.printMatrix();
            }
            if (down_state != null)
            {
                Node node_after_down_operation = new Node(down_state, 1);
                control_queue.append(node_after_down_operation);
                Console.WriteLine("Down:");
                node_after_down_operation.printMatrix();
            }
            if (left_state != null)
            {
                Node node_after_left_operation = new Node(left_state, 1);
                control_queue.append(node_after_left_operation);
                Console.WriteLine("Left:");
                node_after_left_operation.printMatrix();
            }

        }

        public int[,] AfterPerformingUpState(int[,] state)
        {
            int[,] requested_state = state.Clone() as int[,];

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if (requested_state[i, j] == 0)
                    {
                        if (i == (this.height - 1))
                        {
                            return null; // We cannot perform such an operation. 0 (Blank) is not on the very right of the matrix.
                        }
                        else
                        {
                            requested_state[i, j] = requested_state[i + 1, j]; // Here we can perform such an operation so we are returning new state
                            requested_state[i + 1, j] = 0;
                            return requested_state; // We have already successfully satisfied the purpose of this cycle so there is no need to look for other blank
                        }
                    }
                }
            }
            return requested_state;
        }

        public int[,] AfterPerformingRightState(int[,] state)
        {
            int[,] requested_state = state.Clone() as int[,];

            for (int i = 0; i < this.height; i++) {
                for (int j = 0; j < this.width; j++) {
                    if(requested_state[i, j] == 0)
                    {
                        if (j == 0) {
                            return null; // We cannot perform such an operation. 0 (Blank) is not on the very right of the matrix.
                        } else {
                            requested_state[i, j] = requested_state[i, j - 1]; // Here we can perform such an operation so we are returning new state
                            requested_state[i, j - 1] = 0;
                            return requested_state; // We have already successfully satisfied the purpose of this cycle so there is no need to look for other blank
                        }
                    }
                }
            }
            return requested_state;
        }

        public int[,] AfterPerformingDownState(int [,] state)
        {
            int[,] requested_state = state.Clone() as int[,];

            for (int i = 0; i < this.height; i++) {
                for (int j = 0; j < this.width; j++) {
                    if(requested_state[i, j] == 0)
                    {
                        if (i == 0) {
                            return null;
                        } else {
                            requested_state[i, j] = requested_state[i - 1, j];
                            requested_state[i - 1, j] = 0;
                            return requested_state;
                        }
                    }
                }
            }
            return requested_state;
        }

        public int[,] AfterPerformingLeftState(int[,] state)
        {
            int[,] requested_state = state.Clone() as int[,];

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if (requested_state[i, j] == 0)
                    {
                        if (j == (this.width - 1))
                        {
                            return null; // We cannot perform such an operation. 0 (Blank) is not on the very right of the matrix.
                        }
                        else
                        {
                            requested_state[i, j] = requested_state[i, j + 1]; // Here we can perform such an operation so we are returning new state
                            requested_state[i, j + 1] = 0;
                            return requested_state; // We have already successfully satisfied the purpose of this cycle so there is no need to look for other blank
                        }
                    }
                }
            }
            return requested_state;
        }

    }
}
