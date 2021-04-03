using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;

namespace SOAnswerThis.API.Controllers
{
    public static class NoMad
    {
        public static async Task<string> GetStackOverFlowQuestion(this Page page, string questionId)
        {
            await page.GoToAsync($"https://stackoverflow.com/questions/{questionId}");
            return await page.GetContentAsync();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public QuestionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("questions");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions {Headless = true});
            await using var page = await browser.NewPageAsync();
            
            // var httpResponseMessage = await _httpClient.GetAsync("208105");
            // var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var startIndex = -1;
            string response;
            var random = new Random();
            do
            {
                response = await page.GetStackOverFlowQuestion(random.Next(9999999).ToString());
                startIndex = response.IndexOf("s-prose js-post-body", StringComparison.Ordinal);
            } while (startIndex == -1);

            var closeIndex = response.IndexOf("<div class=\"mt24 mb12\">", startIndex, StringComparison.Ordinal);
            var section = response.Substring(startIndex - 12, closeIndex - startIndex + 12);
            //s-prose js-post-body
            return Ok(new {Question=section, Title= (await page.GetTitleAsync())[..^17]});
        }
    }
}