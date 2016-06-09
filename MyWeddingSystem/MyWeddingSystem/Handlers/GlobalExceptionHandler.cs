using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace MyWeddingSystem.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var reason = context.ExceptionContext.Response != null ?
                context.ExceptionContext.Response.ReasonPhrase :
                "Internal Server Error";

            var code = context.ExceptionContext.Response != null ?
                context.ExceptionContext.Response.StatusCode :
                HttpStatusCode.InternalServerError;

            var resp = new HttpResponseMessage
            {
                Content = new StringContent(context.Exception.Message),
                ReasonPhrase = reason,
                StatusCode = code
            };

            context.Result = new ErrorMessageResult(context.Request, resp);
        }
    }

    public class ErrorMessageResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private HttpResponseMessage _httpResponseMessage;

        public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
        {
            _request = request;
            _httpResponseMessage = httpResponseMessage;

        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_httpResponseMessage);
        }
    }
}