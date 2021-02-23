using System.Collections.Generic;

namespace CleanTestsExample.Tests.Setup
{
    public class WithLowMargin : PropertySetup<decimal>
    {
        public override string PropertyName => nameof(Article.MarginPercentage);

        public override IList<decimal> Values => new List<decimal>
        {
            decimal.MinValue,
            4.999999999M
        };
    }
}