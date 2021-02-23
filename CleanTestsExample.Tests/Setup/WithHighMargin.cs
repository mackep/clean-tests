using System.Collections.Generic;

namespace CleanTestsExample.Tests.Setup
{
    public class WithHighMargin : PropertySetup<decimal>
    {
        public override string PropertyName => nameof(Article.MarginPercentage);

        public override IList<decimal> Values => new List<decimal>
        {
            5,
            decimal.MaxValue
        };
    }
}