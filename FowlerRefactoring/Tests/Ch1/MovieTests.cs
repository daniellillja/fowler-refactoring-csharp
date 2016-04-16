using System;
using System.Runtime.InteropServices;
using FowlerRefactoring.Code.Ch1;
using FowlerRefactoring.Code.Ch1.After;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FowlerRefactoring.Tests.Ch1
{
    public abstract class CustomerTests
    {
        protected Customer _sut;
        private string _name;
        private Movie _movie;
        private Rental _rental;
        private string _output;
        
        public void GivenSut()
        {
            _sut = new Customer(_name);
        }

        private void WhenStatementGenerated()
        {
            _output = _sut.Statement();
        }

        public class CustomerRentsRegularMovie : CustomerTests
        {
            [TestClass]
            public class CustomerRentsRegularMovieLessThan2Days : CustomerRentsRegularMovie
            {
                private void GivenARentalAdded()
                {
                    _movie = new Movie("MovieTitle", Movie.REGULAR);
                    _rental = new Rental(_movie, 2);
                    _sut.AddRental(_rental);
                }

                private void GivenCustomerNameSupplied()
                {
                    _name = "Daniel";
                }

                [TestInitialize]
                public void Init()
                {
                    GivenCustomerNameSupplied();
                    GivenSut();
                    GivenARentalAdded();
                    WhenStatementGenerated();

                    Console.WriteLine(_output);
                }
                

                

                [TestMethod]
                public void ShouldReturnCorrectStatement()
                {
                    Assert.IsTrue(_output.Contains("Rental Record for Daniel\n"));
                    Assert.IsTrue(_output.Contains("\tMovieTitle"));
                    Assert.IsTrue(_output.Contains("MovieTitle\t2\n"));
                    Assert.IsTrue(_output.Contains("Amount owed is 2\n"));
                    Assert.IsTrue(_output.Contains("You earned 1 frequent"));
                }
                
            }

            [TestClass]
            public class CustomerRentsRegularMovieMoreThan2Days : CustomerRentsRegularMovie
            {
                [TestInitialize]
                public void Init()
                {
                    GivenCustomerNameSupplied();
                    GivenSut();
                    GivenARentalAdded();
                    WhenStatementGenerated();

                    Console.WriteLine(_output);
                }


                private void GivenARentalAdded()
                {
                    _movie = new Movie("MovieTitle", Movie.REGULAR);
                    _rental = new Rental(_movie, 4);
                    _sut.AddRental(_rental);
                }

                private void GivenCustomerNameSupplied()
                {
                    _name = "Daniel";
                }

                [TestMethod]
                public void ShouldReturnCorrectStatement()
                {
                    Assert.IsTrue(_output.Contains("Rental Record for Daniel\n"));
                    Assert.IsTrue(_output.Contains("\tMovieTitle"));
                    Assert.IsTrue(_output.Contains("MovieTitle\t5\n"));
                    Assert.IsTrue(_output.Contains("Amount owed is 5\n"));
                    Assert.IsTrue(_output.Contains("You earned 1 frequent"));
                }

            }

        }

        [TestClass]
        public class CustomerRentsNewRelease : CustomerTests
        {
            private void GivenARentalAdded()
            {
                _movie = new Movie("MovieTitle", Movie.NEW_RELEASE);
                _rental = new Rental(_movie, 2);
                _sut.AddRental(_rental);
            }

            private void GivenCustomerNameSupplied()
            {
                _name = "Daniel";
            }

            [TestInitialize]
            public void Init()
            {
                GivenCustomerNameSupplied();
                GivenSut();
                GivenARentalAdded();
                WhenStatementGenerated();

                Console.WriteLine(_output);
            }

            [TestMethod]
            public void ShouldProvideCorrectStatement()
            {
                Assert.IsTrue(_output.Contains("Rental Record for Daniel\n"));
                Assert.IsTrue(_output.Contains("\tMovieTitle"));
                Assert.IsTrue(_output.Contains("MovieTitle\t6\n"));
                Assert.IsTrue(_output.Contains("Amount owed is 6\n"));
                Assert.IsTrue(_output.Contains("You earned 2 frequent"));
            }

        }

        [TestClass]
        public class CustomerRentsChildrensLessThan3Days : CustomerTests
        {
            private void GivenARentalAdded()
            {
                _movie = new Movie("MovieTitle", Movie.CHILDRENS);
                _rental = new Rental(_movie, 2);
                _sut.AddRental(_rental);
            }

            private void GivenCustomerNameSupplied()
            {
                _name = "Daniel";
            }

            [TestInitialize]
            public void Init()
            {
                GivenCustomerNameSupplied();
                GivenSut();
                GivenARentalAdded();
                WhenStatementGenerated();

                Console.WriteLine(_output);
            }

            [TestMethod]
            public void ShouldProvideCorrectStatement()
            {
                Assert.IsTrue(_output.Contains("Rental Record for Daniel\n"));
                Assert.IsTrue(_output.Contains("\tMovieTitle"));
                Assert.IsTrue(_output.Contains("MovieTitle\t1.5\n"));
                Assert.IsTrue(_output.Contains("Amount owed is 1.5\n"));
                Assert.IsTrue(_output.Contains("You earned 1 frequent"));
            }

        }

        [TestClass]
        public class CustomerRentsChildrensMoreThan3Days : CustomerTests
        {
            private void GivenARentalAdded()
            {
                _movie = new Movie("MovieTitle", Movie.CHILDRENS);
                _rental = new Rental(_movie, 4);
                _sut.AddRental(_rental);
            }

            private void GivenCustomerNameSupplied()
            {
                _name = "Daniel";
            }

            [TestInitialize]
            public void Init()
            {
                GivenCustomerNameSupplied();
                GivenSut();
                GivenARentalAdded();
                WhenStatementGenerated();

                Console.WriteLine(_output);
            }

            [TestMethod]
            public void ShouldProvideCorrectStatement()
            {
               Assert.IsTrue(_output.Contains("Rental Record for Daniel\n"));
                Assert.IsTrue(_output.Contains("\tMovieTitle"));
                Assert.IsTrue(_output.Contains("MovieTitle\t3\n"));
                Assert.IsTrue(_output.Contains("Amount owed is 3\n"));
                Assert.IsTrue(_output.Contains("You earned 1 frequent"));
            }

        }

    }
}
