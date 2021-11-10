using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NUnit.Framework;
using System;

namespace Tests
{
    public abstract class TestBase<TSut>
        where TSut : class
    {
        bool _recordException;

        protected TSut Sut { get; private set; }

        protected IFixture Fixture { get; private set; }

        protected Exception ThrownException { get; private set; }

        protected TestBase()
        {
            _recordException = false;

            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
        }

        [OneTimeSetUp]
        protected virtual void SetUp()
        {
            Given();

            try
            {
                Sut = CreateSystemUnderTest();

                When();
            }
            catch (Exception ex)
            {
                if (_recordException)
                    ThrownException = ex;
                else
                    throw;
            }
        }

        protected virtual TSut CreateSystemUnderTest()
            => Fixture.Create<TSut>();

        protected virtual void Given() { }

        protected virtual void When() { }

        protected void RecordAnyExceptionsThrown()
        {
            _recordException = true;
        }
    }
}
