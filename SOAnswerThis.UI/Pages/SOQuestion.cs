using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace SOAnswerThis.UI.Pages
{
    public partial class SOQuestion
    {
        private class PageInfo
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }
            [JsonPropertyName("question")]
            public string Question { get; set; }
        }
        [Inject] public IHttpClientFactory HttpClientFactory { get; set; }

        private PageInfo _pageInfo;
        private async Task<PageInfo> GetRandomQuestion()
        {
            var client = HttpClientFactory.CreateClient("questionapi");
            var httpResponseMessage = await client.GetAsync("");
            var returnedText = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(returnedText);
            return JsonSerializer.Deserialize<PageInfo>(returnedText);
        }

        private async void OnNextQuestionClick()
        {
            _pageInfo = null;
            StateHasChanged();
            _pageInfo = await GetRandomQuestion();
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
             _pageInfo = await GetRandomQuestion();
            await base.OnInitializedAsync();
        }
    }
}