using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace CleanTestsExample.Tests
{
    /*
     * These tests illustrate how hard-to-read, highly coupled, fragile unit tests may
     * look.
     *
     * Adding a new constructor argument in Article.cs or adding an additional
     * dependency in ArticleService.cs will break tests.
     */
    public class DirtyTests
    {
        [Theory]
        [InlineData("-123456789")]
        [InlineData("4,99999999")]
        public async Task IsPromoted_returns_false_for_low_margin_article(
            string marginPercentage)
        {
            var article = new Article(Guid.NewGuid(), 123, 456,
                Convert.ToDecimal(marginPercentage));
            var stockService = Substitute.For<IStockService>();
            var repository = Substitute.For<IArticleRepository>();
            repository.GetById(article.Id).Returns(article);
            stockService.GetQuantity(article.Id).Returns(1);

            var sut = new ArticleService(stockService, repository);

            Assert.False(await sut.IsPromoted(article.Id));
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public async Task IsPromoted_returns_false_for_article_not_in_stock(int qty)
        {
            var article = new Article(Guid.NewGuid(), 123, 456, 789);
            var stockService = Substitute.For<IStockService>();
            var repository = Substitute.For<IArticleRepository>();
            repository.GetById(article.Id).Returns(article);
            stockService.GetQuantity(article.Id).Returns(qty);

            var sut = new ArticleService(stockService, repository);

            Assert.False(await sut.IsPromoted(article.Id));
        }

        [Fact]
        public async Task IsPromoted_returns_false_for_article_not_on_sale()
        {
            var article = new Article(Guid.NewGuid(), 123, 0, 789);
            var stockService = Substitute.For<IStockService>();
            var repository = Substitute.For<IArticleRepository>();
            repository.GetById(article.Id).Returns(article);
            stockService.GetQuantity(article.Id).Returns(1);

            var sut = new ArticleService(stockService, repository);

            Assert.False(await sut.IsPromoted(article.Id));
        }

        [Fact]
        public async Task IsPromoted_returns_false_for_missing_article()
        {
            var id = Guid.NewGuid();
            var stockService = Substitute.For<IStockService>();
            var repository = Substitute.For<IArticleRepository>();
            repository.GetById(id).Returns((Article)null);

            var sut = new ArticleService(stockService, repository);

            Assert.False(await sut.IsPromoted(id));
        }
        
        [Theory]
        [InlineData(5)]
        [InlineData(int.MaxValue)]
        public async Task IsPromoted_returns_true_for_in_stock_article_with_good_margin_on_sale(
            decimal marginPercentage)
        {
            var article = new Article(Guid.NewGuid(), 123, 456, marginPercentage);
            var stockService = Substitute.For<IStockService>();
            var repository = Substitute.For<IArticleRepository>();
            repository.GetById(article.Id).Returns(article);
            stockService.GetQuantity(article.Id).Returns(1);

            var sut = new ArticleService(stockService, repository);

            Assert.True(await sut.IsPromoted(article.Id));
        }
    }
}