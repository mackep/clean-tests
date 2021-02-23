using System;

namespace CleanTestsExample
{
    public class Article
    {
        public Guid Id { get; }

        public decimal Price { get; }
        public decimal DiscountPercentage { get; }
        public decimal MarginPercentage { get; }

        public Article(Guid id, decimal price, decimal discountPercentage, decimal marginPercentage)
        {
            Id = id;
            Price = price;
            DiscountPercentage = discountPercentage;
            MarginPercentage = marginPercentage;
        }

        public bool IsOnSale => DiscountPercentage > 0;
    }
}
