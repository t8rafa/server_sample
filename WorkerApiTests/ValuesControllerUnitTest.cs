using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerAPI.Controllers;
using WorkerAPI.Models;

namespace WorkerApiTests
{
    [TestClass]
    public class ValuesControllerUnitTest
    {
        [TestMethod]
        public void PostNullArgumentShouldReturnErrorResponse()
        {
            var controller = new ValuesController();
            var actual = controller.Post(null);

            Assert.AreEqual("1", actual.Code);
            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Incorrect arguments", actual.Message);
        }


        [TestMethod]
        public void PostEmptyNameShouldReturnErrorResponse()
        {
            var controller = new ValuesController();
            var actual = controller.Post(new Candidate());

            Assert.AreEqual("1", actual.Code);
            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Incorrect arguments", actual.Message);
        }


        [TestMethod]
        public void PostEmptyEmailShouldReturnErrorResponse()
        {
            var controller = new ValuesController();
            var actual = controller.Post(new Candidate());

            Assert.AreEqual("1", actual.Code);
            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Incorrect arguments", actual.Message);
        }

        [TestMethod]
        public void PostAndCantSendEmail()
        {
            var candidate = new Candidate()
            {
                Name = "Mock",
                Email = "mock@mock.com"
            };

            var controller = new ValuesController();
            controller.sender = new Mocks.MockSender()
            {
                IsValidReturn = false
            };

            var actual = controller.Post(candidate);

            Assert.AreEqual("2", actual.Code);
            Assert.AreEqual("error", actual.Status);
            Assert.AreEqual("Can't send email", actual.Message);
        }

        [TestMethod]
        public void PostAndSendEmail()
        {
            var candidate = new Candidate()
            {
                Name = "Mock",
                Email = "mock@mock.com"
            };

            var controller = new ValuesController();
            controller.sender = new Mocks.MockSender()
            {
                IsValidReturn = true
            };

            var actual = controller.Post(candidate);

            Assert.AreEqual("0", actual.Code);
            Assert.AreEqual("success", actual.Status);
            Assert.AreEqual("Success", actual.Message);
        }
    }
}
