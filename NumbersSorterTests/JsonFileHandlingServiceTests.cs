namespace NumbersSorterTests
{
    using System.IO;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using NumbersSorter.Models;
    using NumbersSorter.Services;
    using NUnit.Framework;

    public class JSONFileHandlingServiceTestsWithSetup
    {
        private readonly string _filesPath = "D:\\VisualStudioProjects\\NumbersSorter\\NumbersSorterTests\\TestResults\\";
        private readonly string _fileName = "TestResults.txt";

        [SetUp]
        public void SetUp()
        {
            if (!Directory.Exists(_filesPath))
            {
                Directory.CreateDirectory(_filesPath);
            }
        }

        [TearDown]
        public void Dispose()
        {
            if (File.Exists(Path.Combine(_filesPath, _fileName)))
            {
                File.Delete(Path.Combine(_filesPath, _fileName));
            }
        }

        [Test]
        public void TryWriteToFile_WhenCalledWithValidSource_ShouldNotFail()
        {
            ILogger<JsonFileHandlingService> loggerMock = Mock.Of<ILogger<JsonFileHandlingService>>();
            Mock<IOptions<FileConfig>> fileConfigMock = new Mock<IOptions<FileConfig>>();
            fileConfigMock.SetupGet(x => x.Value).Returns(new FileConfig
            {
                Path = _filesPath,
                Name = _fileName
            });

            JsonFileHandlingService fileHandlingService = new JsonFileHandlingService(loggerMock, fileConfigMock.Object);

            string source = "ABC Test";
            Assert.IsTrue(fileHandlingService.TryWriteToFile(source, out string error));
            Assert.IsNull(error);
        }

        [Test]
        public void TryWriteToFile_WhenWriteSucceeds_TryReadFileShouldReturnExpectedResult()
        {
            ILogger<JsonFileHandlingService> loggerMock = Mock.Of<ILogger<JsonFileHandlingService>>();
            Mock<IOptions<FileConfig>> fileConfigMock = new Mock<IOptions<FileConfig>>();
            fileConfigMock.SetupGet(x => x.Value).Returns(new FileConfig
            {
                Path = _filesPath,
                Name = _fileName
            });

            JsonFileHandlingService fileHandlingService = new JsonFileHandlingService(loggerMock, fileConfigMock.Object);

            float[] source = new float[] { 1, 5, 6, 87, 9, 0 };
            string expected = "[1.0,5.0,6.0,87.0,9.0,0.0]";
            bool writeResult = fileHandlingService.TryWriteToFile(source, out string error);

            Assert.IsTrue(writeResult);
            Assert.IsNull(error);

            bool readResult = fileHandlingService.TryReadFile(out error, out string readReturnedResult);

            Assert.IsTrue(readResult);
            Assert.AreEqual(expected, readReturnedResult);
        }

        [Test]
        public void TryWriteToFile_WhenDirectoryPathIsMissing_ShouldReturnError()
        {
            ILogger<JsonFileHandlingService> loggerMock = Mock.Of<ILogger<JsonFileHandlingService>>();
            Mock<IOptions<FileConfig>> fileConfigMock = new Mock<IOptions<FileConfig>>();
            fileConfigMock.SetupGet(x => x.Value).Returns(new FileConfig
            {
                Path = null,
                Name = _fileName
            });

            JsonFileHandlingService fileHandlingService = new JsonFileHandlingService(loggerMock, fileConfigMock.Object);

            string source = "ABC Test";
            bool writeResult = fileHandlingService.TryWriteToFile(source, out string error);

            Assert.IsFalse(writeResult);
            Assert.IsNotNull(error);
        }

        [Test]
        public void TryWriteToFile_WhenFileNameIsMissing_ShouldReturnError()
        {
            ILogger<JsonFileHandlingService> loggerMock = Mock.Of<ILogger<JsonFileHandlingService>>();
            Mock<IOptions<FileConfig>> fileConfigMock = new Mock<IOptions<FileConfig>>();
            fileConfigMock.SetupGet(x => x.Value).Returns(new FileConfig
            {
                Path = _filesPath,
                Name = null
            });

            JsonFileHandlingService fileHandlingService = new JsonFileHandlingService(loggerMock, fileConfigMock.Object);

            string source = "ABC Test";
            bool writeResult = fileHandlingService.TryWriteToFile(source, out string error);

            Assert.IsFalse(writeResult);
            Assert.IsNotNull(error);
        }
    }
}
