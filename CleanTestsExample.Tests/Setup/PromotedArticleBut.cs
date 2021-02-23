using System;
using System.Collections.Generic;

namespace CleanTestsExample.Tests.Setup
{
    public class PromotedArticleBut<T> : IAggregateSetup
    {
        public IEnumerable<Type> AggregateTypes => new List<Type>
        {
            typeof(T),
            typeof(KnownArticle),
            typeof(OnSale),
            typeof(InStock),
            typeof(WithHighMargin),
            typeof(T)
        };
    }
}