﻿using Semesterprojekt2.Models;
namespace Semesterprojekt2.MockData.Shop
{
    public class MockProducts
    {
        private static List<Product> products = new List<Product>() 
        {
            new Product("Lavender Shampoo", 59.5, Product.ProductType.Soap, "Nivea", 1,"Shampoo with smell of lavender"),
            new Product("Lever Treats", 150, Product.ProductType.Treats, "Dog Treats", 200,"Treats with a taste of lever"),
            new Product("Squeaky Cat", 120, Product.ProductType.Toy, "ToysRUs", 1,"A toy cat, that makes a squeaky sound when the dog bites it.")
        };

        public static List<Product> GetMockProducts() { return products; }
    }
}
