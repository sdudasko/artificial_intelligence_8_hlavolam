using System;
namespace artificial_intelligence_8_hlavolam
{
    public class TestCase
    {
        public TestCase()
        {
        }

        public void test_zadanie()
        {
            Algorithm.width = 3;
            Algorithm.height = 3;

            Algorithm algorithm = new Algorithm();

            Algorithm.starting_state = new int[3, 3] { // Current state
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 },
            };

            Algorithm.satisfiable_state = new int[3, 3] { // Final state
                { 2, 7, 3 },
                { 4, 6, 8 },
                { 1, 5, 0 },
            };

            Algorithm.starting_state_order = new int[9];
            Algorithm.satisfiable_state_order = new int[9];

            algorithm.Handle();
        }

        public void test_zadanie_reversed()
        {
            Algorithm.width = 3;
            Algorithm.height = 3;

            Algorithm algorithm = new Algorithm();

            Algorithm.starting_state = new int[3, 3] { // Current state
                { 2, 7, 3 },
                { 4, 6, 8 },
                { 1, 5, 0 },
            };

            Algorithm.satisfiable_state = new int[3, 3] { // Final state
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 },
            };

            Algorithm.starting_state_order = new int[9];
            Algorithm.satisfiable_state_order = new int[9];

            algorithm.Handle();
        }

        public void test_unsolvable_puzzle()
        {
            Algorithm.width = 3;
            Algorithm.height = 3;

            Algorithm algorithm = new Algorithm();

            Algorithm.starting_state = new int[3, 3] { // Current state
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 },
            };

            Algorithm.satisfiable_state = new int[3, 3] { // Final state
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 8, 7, 0 },
            };

            Algorithm.starting_state_order = new int[9];
            Algorithm.satisfiable_state_order = new int[9];

            algorithm.Handle();
        }

        public void test_MxN_puzzle()
        {
            Algorithm.width = 4;
            Algorithm.height = 3;
            Algorithm algorithm = new Algorithm();

            Algorithm.starting_state = new int[3, 4] { // Current state
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 0 },
            };

            Algorithm.satisfiable_state = new int[3, 4] { // Final state
                { 1, 2, 3, 4 },
                { 5, 7, 0, 8 },
                { 9, 6, 10, 11 },
            };

            Algorithm.starting_state_order = new int[12];
            Algorithm.satisfiable_state_order = new int[12];

            algorithm.Handle();
        }
        public void test_NxM_puzzle()
        {
            Algorithm.width = 3;
            Algorithm.height = 5;
            Algorithm algorithm = new Algorithm();

            Algorithm.starting_state = new int[5, 3] { // Current state
                { 1, 2, 10 },
                { 3, 4, 11 },
                { 5, 6, 12 },
                { 7, 8, 13 },
                { 9, 0, 14 },
            };

            Algorithm.satisfiable_state = new int[5, 3] { // Final state
                { 0, 1, 10 },
                { 3, 2, 11 },
                { 5, 4, 12 },
                { 7, 6, 13 },
                { 9, 8, 14 },
            };

            Algorithm.starting_state_order = new int[15];
            Algorithm.satisfiable_state_order = new int[15];

            algorithm.Handle();
        }

        public void test_4x4_puzzle()
        {
            Algorithm.width = 4;
            Algorithm.height = 4;

            Algorithm algorithm = new Algorithm();

            Algorithm.starting_state = new int[4, 4] { // Current state
                { 1, 2, 3, 9 },
                { 4, 5, 6, 10 },
                { 7, 8, 11, 15 },
                { 12, 13, 14, 0 },
            };

            Algorithm.satisfiable_state = new int[4, 4] { // Final state
                { 0, 1, 2, 3 },
                { 4, 5, 6, 9 },
                { 7, 8, 11, 10 },
                { 12, 13, 14, 15 },
            };

            Algorithm.starting_state_order = new int[16];
            Algorithm.satisfiable_state_order = new int[16];

            algorithm.Handle();
        }
    }
}
