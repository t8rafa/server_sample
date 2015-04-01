using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorkerAPI.Mail.Mandrill;

namespace WorkerApiTests.Mocks
{
    public class MockApiRequestStatusCodeOk : IApiRequest
    {
        public string Domain { get; set; }


        public HttpWebResponse ExecuteWithJson(string json)
        {
            var expected = new MessageResponse()
            {
                Status = "sent",
                Message = "Sent email"
            };

            var expectedJson = JsonConvert.SerializeObject(expected);
            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<HttpWebResponse>();
            response.Setup(r => r.StatusCode).Returns(HttpStatusCode.OK);
            response.Setup(r => r.GetResponseStream()).Returns(responseStream);

            return response.Object;
        }
    }

    public class MockApiRequestReturnList : IApiRequest
    {
        public string Domain { get; set; }


        public HttpWebResponse ExecuteWithJson(string json)
        {
            var message = new MessageResponse()
            {
                Status = "sent",
                Message = "Sent email"
            };

            var expected = new List<MessageResponse>() { message };

            var expectedJson = JsonConvert.SerializeObject(expected);
            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<HttpWebResponse>();
            response.Setup(r => r.StatusCode).Returns(HttpStatusCode.OK);
            response.Setup(r => r.GetResponseStream()).Returns(responseStream);

            return response.Object;
        }
    }

    public class MockApiRequestStatusCodeInternalServerError : IApiRequest
    {
        public string Domain { get; set; }


        public HttpWebResponse ExecuteWithJson(string json)
        {
            var response = new Mock<HttpWebResponse>();
            response.Setup(r => r.StatusCode).Returns(HttpStatusCode.InternalServerError);

            return response.Object;
        }
    }

    public class MockApiRequestWebException : IApiRequest
    {
        public string Domain { get; set; }


        public HttpWebResponse ExecuteWithJson(string json)
        {
            throw new WebException();
        }
    }

    public class MockApiRequestException : IApiRequest
    {
        public string Domain { get; set; }


        public HttpWebResponse ExecuteWithJson(string json)
        {
            throw new Exception();
        }
    }
}
