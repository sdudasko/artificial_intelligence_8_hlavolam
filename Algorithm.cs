using System;
using System.Collections.Generic;
using System.Linq;

namespace artificial_intelligence_8_hlavolam
{
    public class Algorithm
    {
        public static int width = 4;
        public static int height = 3;
        public static int nodes_created = 0; // For further statistics


        string[] _operators = { // We'll translate later, we don't want to map by strings so we use indexes in the project
            "UP", // 0.
            "RIGHT", // 1.
            "DOWN", // 2.
            "LEFT", // 3.
        };
        string[] _operators_backwards = { // When we go from end node, we need to make inversed operations to get consecutive solution
            "DOWN", // 0.
            "LEFT", // 1.
            "UP", // 2.
            "RIGHT", // 3.
        };

        public static int[,] starting_state = new int[3, 4] { // Current state
            { 1, 2, 3, 9 },
            { 4, 5, 6, 11 },
            { 7, 8, 0, 10 },
        };
        public static int[,] satisfiable_state = new int[3, 4] { // Final state
            { 2, 7, 3, 9 },
            { 4, 6, 8, 10 },
            { 1, 5, 0, 11 },
        };

        /**
         * 0 is on the 8th place
         * 1 on the 6th place
         * 2 on the 0th place
         * 3 on the 2nd place ...
         * Example: [8, 0, 1, 2, 3, 4, 5, 6, 7]
         */
        public static int[] starting_state_order = new int[12];
        public static int[] satisfiable_state_order = new int[12];

        /**
         * We are setting initial positions of items, eg. 0 => 8 means that 0 is on the 8th position
         * More in initiation of property "starting_state_order" & "satisfiable_state_order" above.
         * The reason for this is to save time in heuristics function of calculating distance of the item matrix's position
         * from the final destination in matrix.
         */
        public void set_state_orders()
        {
            for (int i = 0; i < Algorithm.height * Algorithm.width; i++)
            {
                Algorithm.starting_state_order[i] = 0;
                Algorithm.satisfiable_state_order[i] = 0;
            }

            int f = 0;

            // It is cubic loop but we are breaking in regularly and it does not perform many iteration. Only called in the initiaion of the algorithm.
            for (int p = 0; p < Algorithm.width * Algorithm.height; p++)
            {
                for (int i = 0; i < Algorithm.height; i++)
                {
                    for (int j = 0; j < Algorithm.width; j++)
                    {

                        if (Algorithm.starting_state[i, j] == p)
                        {
                            Algorithm.starting_state_order[p] = f;
                            
                            break;
                        }
                        f++;
                    }
                }
                f = 0;
            }
            
            for (int p = 0; p < Algorithm.height * Algorithm.width; p++)
            {
                for (int i = 0; i < Algorithm.height; i++)
                {
                    for (int j = 0; j < Algorithm.width; j++)
                    {

                        if (Algorithm.satisfiable_state[i, j] == p)
                        {
                            Algorithm.satisfiable_state_order[p] = f;

                            break;
                        }
                        f++;
                    }
                }
                f = 0;
            }
        }

        /**
         * Performing algorighm. Driving program function.
         */ 
        public void Handle()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); // Statistics

            this.set_state_orders();

            // We are performing algorithm "Obojstranneho hladania" so we need 2 nodes and 2 queues to start with

            Queue starting_queue = new Queue();
            Queue ending_queue = new Queue();

            Node starting_node = new Node(Algorithm.starting_state, -1, 0, null, true);
            Node ending_node = new Node(Algorithm.satisfiable_state, -1, 0, null, false);

            starting_queue.append(starting_node);
            ending_queue.append(ending_node);

            Node next_to_handle = starting_queue.getFromHeap();
            Node next_to_handle_from_end = ending_queue.getFromHeap();

            int f = 0;
            bool check_old_states = false;
            int number_of_equal_states = 0;
            var elapsedMs = watch.ElapsedMilliseconds;

            while (next_to_handle != null)
            {
                f++;

                // From each - final and starting node we are creating new states
                if (this.createNewStates(next_to_handle, starting_queue, true))
                {
                    break;
                }
                if (this.createNewStates(next_to_handle_from_end, ending_queue, false))
                {
                    break;
                }

                if (f % 20000 == 0)
                {
                    check_old_states = true;
                }
                if (f % 22000 == 0) // 100 after first 3000, 200 after next 3000, 400 after next, etc.
                {
                    check_old_states = false;
                    number_of_equal_states = 0;
                }
                if (check_old_states)
                {
                    if (starting_queue.check_in_old_states_for_state(starting_queue.getFromHeap()) && ending_queue.check_in_old_states_for_state(ending_queue.getFromHeap()))
                    {
                        number_of_equal_states++;
                    }
                }

                if ((number_of_equal_states != 0) && (number_of_equal_states % 200 == 0))
                {
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine($"Could not find a solution.");
                    break;;
                }

                // We are, in fact, not removing nodes, just setting them to unhandled so we can look back for the old states
                starting_queue.remove(next_to_handle);
                ending_queue.remove(next_to_handle_from_end);

                // Getting item with the lowest cost from "heap" (Not original heap structure, just custom lookalike - queue)
                next_to_handle = starting_queue.getFromHeap();

                next_to_handle_from_end = ending_queue.getFromHeap();

                // After each creating new nodes before nesting any deeper we check if our searches met
                if (this.CheckIfFoundSolutionsMet(next_to_handle, next_to_handle_from_end))
                {
                    Console.WriteLine("\n\nSolution:\n");
                    this.print_solution(next_to_handle, next_to_handle_from_end);
                    break;
                }

            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Execution Time: {elapsedMs} ms");
            Console.WriteLine("Nodes created: " + Algorithm.nodes_created);
        }

        public bool createNewStates(Node starting_node, Queue control_queue, bool from_starting)
        {
            // We check if we can create new state
            int[,] up_state = this.AfterPerformingUpState(starting_node.state);
            int[,] right_state = this.AfterPerformingRightState(starting_node.state);
            int[,] down_state = this.AfterPerformingDownState(starting_node.state);
            int[,] left_state = this.AfterPerformingLeftState(starting_node.state);

            // We also save depth for more percise heuristics
            int givenDepth = 0;
            Node parent = starting_node.parent_node;
            while (parent != null)
            {
                parent = parent.parent_node;
                givenDepth++;
            }

            // If we have created the new state, we create the node with such a state, with the belonging cost
            if (up_state != null) // There is an option to perform up, so we are creating new node for the up operation
            {
                Node node_after_up_operation = new Node(up_state, 0, givenDepth, starting_node, from_starting);
                control_queue.append(node_after_up_operation);
            }
            if (right_state != null)
            {
                Node node_after_right_operation = new Node(right_state, 1, givenDepth, starting_node, from_starting);
                control_queue.append(node_after_right_operation);
            }
            if (down_state != null)
            {
                Node node_after_down_operation = new Node(down_state, 2, givenDepth, starting_node, from_starting);
                control_queue.append(node_after_down_operation);
            }
            if (left_state != null)
            {
                Node node_after_left_operation = new Node(left_state, 3, givenDepth, starting_node, from_starting);
                control_queue.append(node_after_left_operation);
            }

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

        public bool CheckIfFoundSolutionsMet(Node starting, Node ending)
        {
            for (int i = 0; i < Algorithm.height; i++)
            {
                for (int j = 0; j < Algorithm.width; j++)
                {
                    if (starting.state[i, j] != ending.state[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void print_solution(Node next_to_handle, Node next_to_handle_from_end)
        {
            List<int> starting_operations = new List<int>();
            List<int> ending_operations = new List<int>();

            Node start_parent = next_to_handle.parent_node;
            starting_operations.Add(next_to_handle.used_operator);
            int number_of_steps = 2;

            while (start_parent != null)
            {
                if (start_parent.used_operator != -1)
                {
                    starting_operations.Add(start_parent.used_operator);
                }
                start_parent = start_parent.parent_node;
            }

            Node end_parent = next_to_handle_from_end.parent_node;

            ending_operations.Add(next_to_handle_from_end.used_operator);

            while (end_parent != null)
            {
                if (end_parent.used_operator != -1)
                {
                    ending_operations.Add(end_parent.used_operator);
                }
                end_parent = end_parent.parent_node;
            }
            starting_operations.Reverse();

            foreach (int op in starting_operations)
            {
                number_of_steps++;
                Console.WriteLine(this._operators[op]);
            }
            //Console.WriteLine("Second search:"); // Uncomment if you want to see devision of searches
            foreach (int op in ending_operations)
            {
                number_of_steps++;
                Console.WriteLine(this._operators_backwards[op]);
            }
            Console.WriteLine($"Number of steps: {number_of_steps}");
        }
    }
}
