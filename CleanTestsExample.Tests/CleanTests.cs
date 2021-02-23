using System.Threading.Tasks;
using CleanTestsExample.Tests.Setup;
using Xunit;

namespace CleanTestsExample.Tests
{
    /*
     * These tests illustrate a different way of designing the tests found
     * in DirtyTests.cs. The Arrange-phase is composed using a number of
     * type arguments provided to the Given attribute.
     *
     * Due to the raised abstraction level and encapsulation of details
     * in the Arrange phase, the tests are both shorter and (hopefully!)
     * also easier to read and understand. Unimportant details such as
     * magic numbers are no longer visible in the tests, and there is
     * significantly less coupling to classes from the CleanTestsExample
     * project.
     *
     * Adding a new constructor argument in Article.cs or adding a
     * dependency to a new interface in ArticleService.cs will NOT break
     * any tests.
     */
    public class CleanTests
    {
        [Theory]
        [Given(typeof(KnownArticle), typeof(OnSale), typeof(InStock), typeof(WithLowMargin))]
        public async Task IsPromoted_returns_false_for_low_margin_article(
            ArticleService sut, Article article)
        {
            Assert.False(await sut.IsPromoted(article.Id));
        }

        [Theory]
        [Given(typeof(KnownArticle), typeof(OnSale), typeof(WithHighMargin), typeof(OutOfStock))]
        public async Task IsPromoted_returns_false_for_article_not_in_stock(
            ArticleService sut, Article article)
        {
            Assert.False(await sut.IsPromoted(article.Id));
        }

        [Theory]
        [Given(typeof(KnownArticle), typeof(WithHighMargin), typeof(InStock), typeof(NotOnSale))]
        public async Task IsPromoted_returns_false_for_article_not_on_sale(
            ArticleService sut, Article article)
        {
            Assert.False(await sut.IsPromoted(article.Id));
        }

        [Theory]
        [Given(typeof(MissingArticle))]
        public async Task IsPromoted_returns_false_for_missing_article(
            ArticleService sut, Article article)
        {
            Assert.False(await sut.IsPromoted(article.Id));
        }

        [Theory]
        [Given(typeof(KnownArticle), typeof(OnSale), typeof(WithHighMargin), typeof(InStock))]
        public async Task IsPromoted_returns_true_for_in_stock_article_with_good_margin_on_sale(
            ArticleService sut, Article article)
        {
            Assert.True(await sut.IsPromoted(article.Id));
        }
    }
}