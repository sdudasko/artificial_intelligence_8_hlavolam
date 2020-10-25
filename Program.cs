using System;

namespace artificial_intelligence_8_hlavolam
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //new Algorithm().Handle();


            // Note that we are working with static properties for saving memory and so we do not have to create new instances and so on.
            // So to prevent any unexpected errors. Run each test on new compilation.

            TestCase tc = new TestCase();

            //tc.test_zadanie();
            //tc.test_zadanie_reversed();
            //tc.test_unsolvable_puzzle();
            tc.test_MxN_puzzle();
            //tc.test_NxM_puzzle();
            //tc.test_4x4_puzzle();

            return;
        }
    }
}