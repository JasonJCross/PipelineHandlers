using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PipelineHandlers.Tests.DefaultImplementations
{
    public class InjectedProcessTests
    {

        public record TestRecord(string Name, int Number, DateTime Date, TestSubRecord SubRecord)
        {
            public string Name { get; set; } = Name;
            public int Number { get; set; } = Number;
            public DateTime Date { get; set; } = Date;
            public TestSubRecord SubRecord { get; set; } = SubRecord;
        };

        public record TestSubRecord(string Name, int Number, DateTime Date)
        {
            public string Name { get; set; } = Name;
            public int Number { get; set; } = Number;
            public DateTime Date { get; set; } = Date;
        }


        public static IEnumerable<object[]> Process_Tests_Data()
        {
            var baseLogic = new BaseLogic<TestRecord>();
            baseLogic.SetupRun(new TryProcess<TestRecord>((TestRecord entity, out TestRecord output) =>
            {
                output = new TestRecord(entity.Name, entity.Number, entity.Date, entity.SubRecord);
                output.Name += " Processed.";
                output.Number = entity.Number + 1;
                output.Date = entity.Date.AddDays(1);
                output.SubRecord.Name += " Updated.";
                output.SubRecord.Number = entity.SubRecord.Number + 10;
                output.SubRecord.Date = entity.SubRecord.Date.AddYears(1);

                return !entity.Name.Equals("Jason", StringComparison.InvariantCultureIgnoreCase);
            }));

            yield return new object[]
            {
                new TestRecord("Name", 12, new DateTime(2003, 3, 18),
                    new TestSubRecord("SubName", 21, new DateTime(2005, 7, 10))),
                baseLogic,
                new TestRecord("Name Processed.", 13, new DateTime(2003, 3, 19),
                    new TestSubRecord("SubName Updated.", 31, new DateTime(2006, 7, 10))),
                true
            };
            yield return new object[]
            {
                new TestRecord("Jason", 12, new DateTime(2003, 3, 18),
                    new TestSubRecord("SubName", 21, new DateTime(2005, 7, 10))),
                baseLogic,
                new TestRecord("Jason Processed.", 13, new DateTime(2003, 3, 19),
                    new TestSubRecord("SubName Updated.", 31, new DateTime(2006, 7, 10))),
                false
            };
        }

        [Theory, MemberData(nameof(Process_Tests_Data))]
        public void Process_Tests(TestRecord entity, ILogic<TestRecord> logic, TestRecord expectedEntity, bool expectedContinue)
        {
            var injectedProcess = new InjectedProcess<TestRecord, ILogic<TestRecord>>(logic);

            var actualEntity = injectedProcess.Process(entity);

            actualEntity.Should().Be(expectedEntity);
            injectedProcess.Continue.Should().Be(expectedContinue);
        }
    }
}