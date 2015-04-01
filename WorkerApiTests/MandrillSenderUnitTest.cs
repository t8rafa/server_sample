using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerAPI.Mail.Mandrill;

namespace WorkerApiTests
{
    [TestClass]
    public class MandrillSenderUnitTest
    {
        [TestMethod]
        public void SendWithNullNameShouldReturnErrorResponse()
        {
            var sender = MandrillSender.Create();

            var actual = sender.Send(null, "mock@mock.com", "Subject", "text");

            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Incorrect arguments", actual.Message);
        }

        [TestMethod]
        public void SendWithNullEmailShouldReturnErrorResponse()
        {
            var sender = MandrillSender.Create();

            var actual = sender.Send("Mock", null, "Subject", "text");

            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Incorrect arguments", actual.Message);
        }

        [TestMethod]
        public void SendWithNullSubjectShouldReturnErrorResponse()
        {
            var sender = MandrillSender.Create();

            var actual = sender.Send("Mock", "mock@mock.com", null, "text");

            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Incorrect arguments", actual.Message);
        }

        [TestMethod]
        public void SendWithNullTextShouldReturnErrorResponse()
        {
            var sender = MandrillSender.Create();

            var actual = sender.Send("Mock", "mock@mock.com", "Subject", null);

            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Incorrect arguments", actual.Message);
        }

        [TestMethod]
        public void SendWithSuccessShouldReturnSentResponse()
        {
            var sender = MandrillSender.Create();
            sender.WebRequest = new Mocks.MockApiRequestStatusCodeOk();

            var actual = sender.Send("Mock", "mock@mock.com", "Subject", "mock text");

            Assert.AreEqual("sent", actual.Status);
            Assert.AreEqual("Sent email", actual.Message);
        }

        [TestMethod]
        public void SendWithSuccessShouldReturnSentResponseForList()
        {
            var sender = MandrillSender.Create();
            sender.WebRequest = new Mocks.MockApiRequestReturnList();

            var actual = sender.Send("Mock", "mock@mock.com", "Subject", "mock text");

            Assert.AreEqual("sent", actual.Status);
            Assert.AreEqual("Sent email", actual.Message);
        }

        [TestMethod]
        public void SendWithMalformedRequestShouldReturnErrorResponse()
        {
            var sender = MandrillSender.Create();
            sender.WebRequest = new Mocks.MockApiRequestStatusCodeInternalServerError();

            var actual = sender.Send("Mock", "mock@mock.com", "Subject", "mock text");

            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Malformed request", actual.Message);
        }

        [TestMethod]
        public void SendWithOfflineServerShouldReturnErrorResponse()
        {
            var sender = MandrillSender.Create();
            sender.WebRequest = new Mocks.MockApiRequestWebException();

            var actual = sender.Send("Mock", "mock@mock.com", "Subject", "mock text");

            Assert.AreEqual("connection_error", actual.Status);
            Assert.AreEqual("Can't connect to server", actual.Message);
        }

        [TestMethod]
        public void SendWithBuggyServerShouldReturnErrorResponse()
        {
            var sender = MandrillSender.Create();
            sender.WebRequest = new Mocks.MockApiRequestException();

            var actual = sender.Send("Mock", "mock@mock.com", "Subject", "mock text");

            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Server is buggy", actual.Message);
        }
    }
}