using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Infrastructure
{
    public class ApiCallFailedException : Exception
    {
        public string Response { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public ApiCallFailedException(IRestResponse response) {
            Response = $"Api call failed with method: {response.Request.Method} with status code: {response.StatusCode}.";
            StatusCode = response.StatusCode;
        }
    }
}
