namespace NumbersSorterTests
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using NumbersSorter.Services;
    using NUnit.Framework;

    public class NumbersSortingServiceTests
    {
        [Test]
        public void TrySortNumbers_SortNumbersInAsscendingOrder_ReturnExpectedResults()
        {
            ILogger<NumbersSortingService> loggerMock = Mock.Of<ILogger<NumbersSortingService>>();
            NumbersSortingService numbersSortingService = new NumbersSortingService(loggerMock);

            float[] source = new float[] { 1, 10, 0, 5.5f };
            numbersSortingService.TrySortNumbers(source, true, out float[] sortedNumbersResult, out string error);
            float[] expected = new float[] { 0, 1, 5.5f, 10 };

            Assert.AreEqual(expected, sortedNumbersResult);
            Assert.IsNull(null);
        }

        [Test]
        public void TrySortNumbers_SortNumbersInDescendingOrder_ReturnExpectedResults()
        {
            ILogger<NumbersSortingService> loggerMock = Mock.Of<ILogger<NumbersSortingService>>();
            NumbersSortingService numbersSortingService = new NumbersSortingService(loggerMock);

            float[] source = new float[] { 1, 10, 0, 5.5f };
            numbersSortingService.TrySortNumbers(source, false, out float[] sortedNumbersResult, out string error);
            float[] expected = new float[] { 10, 5.5f, 1, 0 };

            Assert.AreEqual(expected, sortedNumbersResult);
            Assert.IsNull(error);
        }

        [Test]
        public void TrySortNumbers_WhenSourceIsEmpty_ShouldReturnError()
        {
            ILogger<NumbersSortingService> loggerMock = Mock.Of<ILogger<NumbersSortingService>>();
            NumbersSortingService numbersSortingService = new NumbersSortingService(loggerMock);

            float[] source = new float[0];
            numbersSortingService.TrySortNumbers(source, false, out float[] sortedNumbersResult, out string error);

            Assert.IsNotNull(error);
        }

        [Test]
        public void TrySortNumbers_WhenSourceIsNull_ShouldReturnError()
        {
            ILogger<NumbersSortingService> loggerMock = Mock.Of<ILogger<NumbersSortingService>>();
            NumbersSortingService numbersSortingService = new NumbersSortingService(loggerMock);

            float[] source = null;
            numbersSortingService.TrySortNumbers(source, false, out float[] sortedNumbersResult, out string error);

            Assert.IsNotNull(error);
        }

        [Test]
        public void TrySortNumbers_WhenSourceIsSingleNumber_ReturnExpectedResults()
        {
            ILogger<NumbersSortingService> loggerMock = Mock.Of<ILogger<NumbersSortingService>>();
            NumbersSortingService numbersSortingService = new NumbersSortingService(loggerMock);

            float[] source = new float[] { 10 };
            numbersSortingService.TrySortNumbers(source, false, out float[] sortedNumbersResult, out string error);

            Assert.IsNotNull(sortedNumbersResult);
            Assert.IsNull(error);
        }
    }
}
