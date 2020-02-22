using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientSample.Handlers
{
    public class ValidateAccessTokenHeaderHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-Access-Token"))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("You must supply an access token header called X-Access-Token")
                };
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
