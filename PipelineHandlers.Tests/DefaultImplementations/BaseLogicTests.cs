using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PipelineHandlers.Tests.DefaultImplementations
{
    public class BaseLogicTests
    {
        public static IEnumerable<object[]> TryRun_Tests_Data()
        {
            yield return new object[]
            {
                "Test", // entity
                "Test123", // expectedOutput
                true, // expected
                new TryProcess<object>((object x, out object y) =>
                {
                    var input = x.ToString();
                    y = input + "123";
                    return true;
                })
            };
            yield return new object[]
            {
                (int) 1976, // entity
                (int) 1981, // expectedOutput
                true, // expected
                new TryProcess<object>((object x, out object y) =>
                {
                    var input = (int) x;
                    y = input + 5;
                    return true;
                })
            };
        }


        [Theory, MemberData(nameof(TryRun_Tests_Data))]
        public void TryRun_Tests(object entity, object expectedOutput, bool expected, TryProcess<object> condition)
        {

            var baseLogic = new BaseLogic<object>();
            baseLogic.SetupRun(condition);

            var actual = baseLogic.TryRun(entity, out var actualOutput);

            actual.Should().Be(expected);
            actualOutput.Should().Be(expectedOutput);
        }

    }
}