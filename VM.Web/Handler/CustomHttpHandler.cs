using Blazored.LocalStorage;

namespace VM.Web.Handler
{
    public class CustomHttpHandler(ILocalStorageService localStorageService) : DelegatingHandler(new HttpClientHandler())
    {

        private readonly ILocalStorageService _localStorageService = localStorageService;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = string.Empty;
            try
            {
                token = await _localStorageService.GetItemAsStringAsync("token", cancellationToken);
            }
            catch (Exception ex)
            {
            }

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
