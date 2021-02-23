using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;

namespace CleanTestsExample.Tests.Setup
{
    public class GivenAttribute : AutoDataAttribute
    {
        public GivenAttribute(params Type[] types)
            : base(() =>
            {
                var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

                Apply(types, fixture);

                return fixture;
            })
        {
        }

        private static void Apply(IEnumerable<Type> types, IFixture fixture)
        {
            foreach (var type in types.Where(t => !t.IsAbstract))
            {
                var instance = Activator.CreateInstance(type);
                switch (instance)
                {
                    case IAggregateSetup aggregateSetup:
                        Apply(aggregateSetup.AggregateTypes, fixture);
                        break;
                    case ISpecimenBuilder specimenBuilder:
                        fixture.Customizations.Add(specimenBuilder);
                        break;
                    case ICustomization customization:
                        fixture.Customize(customization);
                        break;
                }
            }
        }
    }
}