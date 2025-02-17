using StepByStep.Sandbox.ExpressionEvaluators;
using System.Text.Json;

namespace StepByStep.Sandbox.Steps.RestApis.HTTPs
{
    internal sealed class HttpStepHandler : IStepHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpStepHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Handle(IStep step, Automat automat, IExpressionEvaluator expressionEvaluator)
        {
            HttpStep httpStep = (HttpStep)step;

            using var httpRequestMessage = new HttpRequestMessage(httpStep.Method, httpStep.Url)
            {
            };

            using var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseContent}.");
            }
            else
            {
                throw new HttpRequestException($"Failed to execute HTTP request. Status code: {httpResponseMessage.StatusCode}");
            }
        }
    }
}
