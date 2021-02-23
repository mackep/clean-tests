using System;
using System.Linq;
using AutoFixture;
using NSubstitute;

namespace CleanTestsExample.Tests.Setup
{
    public class InStock : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var qty = fixture.Create<Generator<int>>().First(v => v > 0);
            fixture.Freeze<IStockService>().GetQuantity(Arg.Any<Guid>()).Returns(qty);
        }
    }
}