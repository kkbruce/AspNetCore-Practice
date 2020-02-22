using Refit;
using System.Threading.Tasks;

namespace HttpClientSample.Services
{
    public interface IBlogApi
    {
            [Get("/search/label/{tagName}")]
            Task<string> GetTagName(string tagName);
    }
}
