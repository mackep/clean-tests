using System.Collections.Generic;

namespace CleanTestsExample.Tests.Setup
{
    public class NotOnSale : PropertySetup<decimal>
    {
        public override string PropertyName => nameof(Article.DiscountPercentage);

        public override IList<decimal> Values => new List<decimal>
        {
            0
        };
    }
}