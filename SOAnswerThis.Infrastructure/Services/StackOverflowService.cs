using System;
using System.Threading;
using System.Threading.Tasks;
using PuppeteerSharp;
using SOAnswerThis.DTO;
using SOAnswerThis.Infrastructure.Services.Abstract;

namespace SOAnswerThis.Infrastructure.Services
{
    public class StackOverflowService : IStackOverflowService
    {
        private readonly Page _page;
        private static readonly Random Random = new();
        public StackOverflowService()
        {
            var browserFetcher = new BrowserFetcher();
            browserFetcher.DownloadAsync().Wait();
            var browser = Puppeteer.LaunchAsync(new LaunchOptions {Headless = true}).Result;
            _page = browser.NewPageAsync().Result;
        }

        public async Task<StackoverflowQuestionDTO> GetRandomStackoverflowQuestion(CancellationToken cancellationToken = default)
        {
            int startIndex;
            string response;
            var random = new Random();
            do
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException(cancellationToken);
                }
                response = await GetStackOverFlowQuestion(random.Next(9999999).ToString());
                startIndex = response.IndexOf("s-prose js-post-body", StringComparison.Ordinal);
            } while (startIndex == -1);

            var closeIndex = response.IndexOf("<div class=\"mt24 mb12\">", startIndex, StringComparison.Ordinal);
            var section = response.Substring(startIndex - 12, closeIndex - startIndex + 12);
            return new StackoverflowQuestionDTO()
            {
                QuestionHtml = section,
                Title = (await _page.GetTitleAsync())[..^17],
                QuestionUrl = _page.Url
            };
        }
        private async Task<string> GetStackOverFlowQuestion(string questionId)
        {
            await _page.GoToAsync($"https://stackoverflow.com/questions/{questionId}");
            return await _page.GetContentAsync();
        }
    }
}