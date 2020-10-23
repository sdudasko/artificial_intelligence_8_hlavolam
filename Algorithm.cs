using System;
namespace artificial_intelligence_8_hlavolam
{
    public class Algorithm
    {
        //Node[] state_tree = new Node[] { };
        public static int width = 3;
        public static int height = 3;
        public static int nodes_created = 0; // For further statistics

        Queue starting_queue = new Queue();
        Queue ending_queue = new Queue();

        string[] _operators = { // We'll translate later, we don't want to map by strings so we use indexes in the project
            "up", // 0.
            "right", // 1.
            "down", // 2.
            "left", // 3.
        };

        int[,] starting_state = new int[3, 3] { // Current state
            { 2, 7, 3 },
            { 4, 6, 8 },
            { 1, 5, 0 },
            
        };

        int[,] satisfiable_state = new int[3, 3] { // Current state
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
        };

        public void Handle()
        {
            //this.checkIfSolvable();

            Node starting_node = new Node(this.starting_state, -1, 0);

            this.starting_queue.append(starting_node);

            Node next_to_handle = this.starting_queue.getFromHeap();
            Console.WriteLine("\n\n\n-----------------n\n\n");
            int f = 0;
            while (next_to_handle != null)
            {
                f++;
                if (this.createNewStates(next_to_handle))
                {
                    break;
                }

                this.starting_queue.remove(next_to_handle);
                next_to_handle = this.starting_queue.getFromHeap();

                if (f == 15000)
                {
                    next_to_handle.printMatrix();
                    return;
                }
            }

            Node parent = next_to_handle.parent_node;
            Console.WriteLine(this._operators[next_to_handle.used_operator]);
            while (parent != null)
            {
                if (parent.used_operator != -1)
                {
                    Console.WriteLine(this._operators[parent.used_operator]);
                }
                parent = parent.parent_node;
            }

            Console.WriteLine("Solution was found!");
            Console.WriteLine("Nodes created: " + Algorithm.nodes_created);

            return;
        }

        public bool createNewStates(Node starting_node)
        {
            // Handle starting node
            int[,] up_state = this.AfterPerformingUpState(starting_node.state);
            int[,] right_state = this.AfterPerformingRightState(starting_node.state);
            int[,] down_state = this.AfterPerformingDownState(starting_node.state);
            int[,] left_state = this.AfterPerformingLeftState(starting_node.state);

            int givenDepth = 0;

            Node parent = starting_node.parent_node;
            while (parent != null)
            {
                parent = parent.parent_node;
                givenDepth++;
            }

            if (up_state != null) // There is an option to perform up, so we are creating new node for the up operation
            {
                Node node_after_up_operation = new Node(up_state, 0, givenDepth, starting_node);
                this.starting_queue.append(node_after_up_operation);
            }
            if (right_state != null) // There is an option to perform right, so we are creating new node for the right operation
            {
                Node node_after_right_operation = new Node(right_state, 1, givenDepth, starting_node);
                this.starting_queue.append(node_after_right_operation);
            }
            if (down_state != null)
            {
                Node node_after_down_operation = new Node(down_state, 2, givenDepth, starting_node);
                this.starting_queue.append(node_after_down_operation);
            }
            if (left_state != null)
            {
                Node node_after_left_operation = new Node(left_state, 3, givenDepth, starting_node);
                this.starting_queue.append(node_after_left_operation);
            }

            if (this.CheckIfFoundTheSolution(starting_node))
            {
                return true;
            }
            //this.starting_queue.remove(starting_node);

            return false;
        }

        public int[,] AfterPerformingUpState(int[,] state)
        {
            int[,] requested_state = state.Clone() as int[,];

            for (int i = 0; i < Algorithm.height; i++)
            {
                for (int j = 0; j < Algorithm.width; j++)
                {
                    if (requested_state[i, j] == 0)
                    {
                        if (i == (Algorithm.height - 1))
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

            for (int i = 0; i < Algorithm.height; i++) {
                for (int j = 0; j < Algorithm.width; j++) {
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

            for (int i = 0; i < Algorithm.height; i++) {
                for (int j = 0; j < Algorithm.width; j++) {
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

            for (int i = 0; i < Algorithm.height; i++)
            {
                for (int j = 0; j < Algorithm.width; j++)
                {
                    if (requested_state[i, j] == 0)
                    {
                        if (j == (Algorithm.width - 1))
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

        public bool CheckIfFoundTheSolution(Node compared_node)
        {
            for (int i = 0; i < Algorithm.height; i++)
            {
                for (int j = 0; j < Algorithm.width; j++)
                {
                    if (compared_node.state[i, j] != this.satisfiable_state[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool checkIfSolvable()
        {
            return true;
        }
    }
}
