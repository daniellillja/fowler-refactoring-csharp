using System;
using System.Collections.Generic;
using System.Linq;

namespace FowlerRefactoring.Code.Ch1.After
{
    public abstract class Price
    {
        public abstract int GetPriceCode();
        public abstract double GetCharge(int daysRented);

        public virtual int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }
    }

    public class ChildrensPrice : Price
    {
        public override int GetPriceCode()
        {
            return Movie.CHILDRENS;
        }

        public override double GetCharge(int daysRented)
        {
            var result = 1.5;
            if (daysRented > 3)
            {
                result += (daysRented - 3) * 1.5;
            }
            return result;
        }
    }

    public class NewReleasePrice : Price
    {
        public override int GetPriceCode()
        {
            return Movie.NEW_RELEASE;
        }

        public override double GetCharge(int daysRented)
        {
            return daysRented * 3;
        }

        public override int GetFrequentRenterPoints(int daysRented)
        {
            return daysRented > 1 ? 2 : 1;
        }
    }

    public class RegularPrice : Price
    {
        public override int GetPriceCode()
        {
            return Movie.REGULAR;
        }

        public override double GetCharge(int daysRented)
        {
            double result = 2;
            if (daysRented > 2)
            {
                result += (daysRented - 2) * 1.5;
            }
            return result;
        }
    }

    public class Movie
    {
        private string _title;
        private Price _price;
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        public Movie(string title, int priceCode)
        {
            _title = title;
            SetPriceCode(priceCode);
        }

        public void SetPriceCode(int arg)
        {
            switch (arg)
            {
                case CHILDRENS:
                    _price = new ChildrensPrice();
                    break;
                case REGULAR:
                    _price = new RegularPrice();
                    break;
                case NEW_RELEASE:
                    _price = new NewReleasePrice();
                    break;
            }
        }

        public string GetTitle()
        {
            return _title;
        }

        public void SetTitle(string arg)
        {
            _title = arg;
        }

        public double GetCharge(int daysRented)
        {
            return _price.GetCharge(daysRented);
        }

        public int GetPriceCode()
        {
            return _price.GetPriceCode();
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            return _price.GetFrequentRenterPoints(daysRented);
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

        public int GetFrequentRenterPoints()
        {
            return GetMovie().GetFrequentRenterPoints(_daysRented);
        }

        public string GetRentalStatement()
        {
            string statementFormat = "\t{0}\t{1}\n";
            var title = GetMovie().GetTitle();
            var rentalAmount = GetRentalAmount();
            return String.Format(statementFormat, title, rentalAmount);
        }

        public double GetRentalAmount()
        {
            return GetMovie().GetCharge(_daysRented);
        }

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
            var statement = new CustomerRentalStatement(this);
            return statement.FormatAsString();

        }
        public string GetRentalLineStatements()
        {
            string result = string.Empty;
            foreach (var rental in _rentals)
            {
                result += rental.GetRentalStatement();
            }
            return result;
        }

        public int GetTotalFrequentRentalPoints()
        {
            return _rentals.Sum(r => r.GetFrequentRenterPoints());
        }

        public  double GetTotalRentalAmount()
        {
            return _rentals.Sum(r => r.GetRentalAmount());
        }


    }

    public class CustomerRentalStatement
    {
        private readonly Customer _customer;

        public CustomerRentalStatement(Customer customer)
        {
            _customer = customer;
            CustomerName = customer.GetName();
            TotalAmount = customer.GetTotalRentalAmount().ToString();
            FrequentRenterPoints = customer.GetTotalFrequentRentalPoints().ToString();
            
        }

        public string CustomerName { get; set; }
        public string TotalAmount { get; set; }
        public string FrequentRenterPoints { get; set; }

        public string FormatAsString()
        {
            var template = "Rental Record for {0}\n{1}Amount owed is {2}\nYou earned {3} frequent renter points";
            return String.Format(template,
                CustomerName,
                _customer.GetRentalLineStatements(),
                TotalAmount,
                FrequentRenterPoints);
        }

    }
}
