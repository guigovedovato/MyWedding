using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace MyWeddingSystem.Handlers
{
    public class ExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
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
        private HttpRequestMessage httpRequestMessage;
        private HttpResponseMessage httpResponseMessage;

        public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
        {
            this.httpRequestMessage = request;
            this.httpResponseMessage = httpResponseMessage;

        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(httpResponseMessage);
        }
    }
}