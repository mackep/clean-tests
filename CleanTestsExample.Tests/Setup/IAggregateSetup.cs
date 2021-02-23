using System;
using System.Collections.Generic;

namespace CleanTestsExample.Tests.Setup
{
    public interface IAggregateSetup
    {
        IEnumerable<Type> AggregateTypes { get; }
    }
}