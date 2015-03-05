using System;
using System.Collections.Generic;

namespace DataModel
{
    internal class DatabasePopulationProvider
    {
        private static readonly Random R = new Random();

        public static List<Customer> GetCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Patrik",
                    LastName = "Fuhrmann",
                    Email = "fuhrmann.patrik@gmail.com",
                    Gender = false,
                    PhoneNumber = "+07426255540",
                    DOB = new DateTime(1990, 6, 18),
                    PasswordHash = "AL9/jmkGTa6QXsOisbWorQWqiNvFOxu7zbNhyY3lUJ+iXcxz5QMu+5Wt/5hJ3GXkUQ==",
                    AddressLine1 = "14 Eleonora Terrace",
                    AddressLine2 = "Lind Road",
                    Town = "Sutton",
                    Postcode = "SM1 4PU",

                }
            };

            return customers;
        }

        public static List<Product> GetProducts()
        {
            var products = new List<Product>();

            for (var i = 1; i <= 20; i++)
            {
                products.Add(new Product
                {
                    Name = "Box Name " + i,
                    Price = (short) R.Next(10, 50),
                    Weight = (short) R.Next(100, 2500),
                    BoxItemsAmount = (short) R.Next(10, 50)
                });
            }

            return products;
        }

        public static List<Warehouse> GetWarehouses()
        {
            var warehouses = new List<Warehouse>
            {
                new Warehouse {Name = "London"},
                new Warehouse {Name = "Manchester"},
                new Warehouse {Name = "Birmingham"},
                new Warehouse {Name = "Leeds"},
                new Warehouse {Name = "Liverpool"},
                new Warehouse {Name = "Southampton"},
                new Warehouse {Name = "Newcastle"},
                new Warehouse {Name = "Nottingham"},
                new Warehouse {Name = "Sheffield "},
                new Warehouse {Name = "Bristol"}
            };

            return warehouses;
        }

        public static List<Stock> GetStocks()
        {
            var stocks = new List<Stock>();

            for (var i = 1; i <= 50; i++)
            {
                stocks.Add(new Stock
                {
                    Product_Id = R.Next(1, 20),
                    Warehouse_Id = R.Next(1, 10),
                    Quantity = R.Next(1, 50)
                });
            }

            return stocks;
        }
    }
}