using Blaise.Cli.Core.Command;
using Blaise.Cli.Core.Interfaces;
using Moq;
using NUnit.Framework;
using StatNeth.Blaise.API.DataInterface;

namespace Blaise.Cli.Tests.Unit.Command
{
    public class ParseDataInterfaceCommandsTests
    {
        private Mock<IBlaiseFileService> _blaiseFileService;
        private ICommandParser _sut;

        [SetUp]
        public void SetupTests()
        {
            _blaiseFileService = new Mock<IBlaiseFileService>();
            _sut = new CommandParser(_blaiseFileService.Object);
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Pass_DataInterface_Arguments_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(ApplicationType applicationType)
        {
            //Arrange
            var args = new[] { "datainterface", "-t" ,$"{applicationType}", "-f", "file.ext" };

            //Act
            _sut.ParseArguments(args);

            //Assert
            _blaiseFileService.Verify(b => b.CreateDataInterfaceFile(applicationType, "file.ext"));
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Pass_DataInterface_Arguments_With_FullNames_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(ApplicationType applicationType)
        {
            //Arrange
            var args = new[] { "datainterface", "--type", $"{applicationType}", "--file", "file.ext" };

            //Act
            _sut.ParseArguments(args);

            //Assert
            _blaiseFileService.Verify(b => b.CreateDataInterfaceFile(applicationType, "file.ext"));
        }

        [Test]
        public void Given_We_Pass_An_Incorrect_ApplicationType_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datainterface", "-t", $"hello", "-f", "file.ext" };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Dont_Pass_In_TypeArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datainterface", "-f", "file.ext" };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Dont_Pass_In_FileArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datainterface", "-t", $"hello" };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }
    }
}
