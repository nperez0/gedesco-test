using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public abstract class AsyncTestBase<TSut>
        where TSut : class
    {
        bool _recordException;

        protected TSut Sut { get; set; }

        protected IFixture Fixture { get; private set; }

        protected Exception ThrownException { get; set; }

        protected AsyncTestBase()
        {
            _recordException = false;

            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
        }

        [OneTimeSetUp]
        protected async virtual Task SetUp()
        {
            await Given()
                .ConfigureAwait(false);

            try
            {
                Sut = await CreateSystemUnderTest()
                    .ConfigureAwait(false);

                await When()
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (_recordException)
                    ThrownException = ex;
                else
                    throw;
            }
        }

        protected virtual Task<TSut> CreateSystemUnderTest()
            => Task.FromResult(Fixture.Create<TSut>());

        protected virtual Task Given() => Task.FromResult(0);

        protected virtual Task When() => Task.FromResult(0);

        protected void RecordAnyExceptionsThrown()
        {
            _recordException = true;
        }
    }
}
