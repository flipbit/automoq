using NUnit.Framework;

namespace Moq
{
    [TestFixture]
    public class AutoMockingTestTest : AutoMockingTest
    {
        private FakeClass @class;

        [SetUp]
        public void SetUp()
        {
            @class = Create<FakeClass>();
        }

        [Test]
        public void TestAutomaticMocking()
        {
            Mock<IFakeService>()
                .Setup(call => call.DoWork("test"))
                .Returns(99);

            var result = @class.Calculate();

            Assert.AreEqual(99, result);
        }
    }
}
