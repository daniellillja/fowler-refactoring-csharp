using System;

namespace FowlerRefactoring.Code.Ch6.Before
{
    public class ExtractMethod
    {
        private string _name;

        public ExtractMethod(string name)
        {
            _name = name;
        }

        public void PrintOwing(double amount)
        {
            PrintBanner();

            // print details
            Console.WriteLine("name:" + _name);
            Console.WriteLine("amount:" + amount);
        }

        private void PrintBanner()
        {
        }
    }
}
