using System;

namespace FowlerRefactoring.Code.Ch6.After
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
            PrintDetails(amount);
        }

        private void PrintDetails(double amount)
        {
            Console.WriteLine("name:" + _name);
            Console.WriteLine("amount:" + amount);
        }

        private void PrintBanner()
        {
        }
    }
}
