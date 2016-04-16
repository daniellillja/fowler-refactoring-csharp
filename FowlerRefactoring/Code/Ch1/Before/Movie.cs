using System.Collections.Generic;

namespace FowlerRefactoring.Code.Ch1.Before
{
    public class Movie
    {
        private string _title;
        private int _priceCode;
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        public Movie(string title, int priceCode)
        {
            _title = title;
            _priceCode = priceCode;
        }

        public int GetPriceCode()
        {
            return _priceCode;
        }

        public void SetPriceCode(int arg)
        {
            _priceCode = arg;
        }

        public string GetTitle()
        {
            return _title;
        }

        public void SetTitle(string arg)
        {
            _title = arg;
        }
    }

    public class Rental
    {
        private readonly Movie _movie;
        private readonly int _daysRented;

        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }

        public int GetDaysRented()
        {
            return _daysRented;
        }

        public Movie GetMovie() { return _movie; }
    }

    public class Customer
    {
        private readonly string _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            _name = name;
        }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string GetName()
        {
            return _name;
        }

        public string Statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;

            var result = "Rental Record for " + GetName() + "\n";
            while (_rentals.GetEnumerator().MoveNext())
            {
                double thisAmount = 0;
                Rental each = _rentals.GetEnumerator().Current;

                // determine amounts for each line
                switch (each.GetMovie().GetPriceCode())
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (each.GetDaysRented() > 2)
                        {
                            thisAmount += (each.GetDaysRented() - 2) * 1.5;
                        }
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += each.GetDaysRented() * 3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (each.GetDaysRented() > 3)
                        {
                            thisAmount += (each.GetDaysRented() - 3) * 1.5;
                        }
                        break;

                }
                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if ((each.GetMovie().GetPriceCode() == Movie.NEW_RELEASE) && each.GetDaysRented() > 1)
                {
                    frequentRenterPoints++;

                }

                // show figures for this rental
                result += "\t" + each.GetMovie().GetTitle() + "\t" + thisAmount + "\n";
                totalAmount += totalAmount;
            }

            // add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points";
            return result;
        }
    }
}