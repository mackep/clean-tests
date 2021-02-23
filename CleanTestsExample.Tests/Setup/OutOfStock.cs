using AutoFixture;
using NSubstitute;

namespace CleanTestsExample.Tests.Setup
{
    public abstract class OutOfStock : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var article = fixture.Freeze<Article>();
            fixture.Freeze<IStockService>().GetQuantity(article.Id).Returns(0);
        }
    }
}