using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerAPI.Mail.Mandrill;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace WorkerApiTests
{
    [TestClass]
    public class MandrillApiRequestUnitTest
    {
        private MessageRequest PrepareRequest(string toName, string toEmail, string subject, string text)
        {
            var to = new To()
            {
                Name = toName,
                Email = toEmail
            };

            var message = new Message()
            {
                FromEmail = "rafael@t8studio.com.br",
                FromName = "Rafael Alencar Trisotto",
                To = new List<To>() { to },
                Subject = subject,
                Text = text
            };

            return new MessageRequest()
            {
                Key = "ib5AeJW6lO2pGgdGN0OMyw",
                Message = message
            };
        }

        [TestMethod]
        public void ExecuteWithCorrectJson()
        {
            var jsonObject = PrepareRequest(
                "Mock",
                "rafael@t8studio.com.br",
                "Testes unitários",
                "Executando os testes unitários"
            );

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var json = JsonConvert.SerializeObject(jsonObject, settings);

            var request = new MandrillApiRequest();
            request.Domain = "messages/send.json";

            var actual = request.ExecuteWithJson(json);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void ExecuteWithWebException()
        {
            var request = new MandrillApiRequest();
            request.Domain = "messages/sendxyz.json";

            var actual = request.ExecuteWithJson(string.Empty);
        }
    }
}
