using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace RustErrorsFixLibrary.Core.OpenAI
{
    internal partial class OpenAIAPI
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public OpenAIAPI(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetCompletion(string content)
        {
            List<Message> messages = new List<Message>();

            var message = new Message() { Role = "user", Content = content };
            // добавляем сообщение в список сообщений
            messages.Add(message);

            // формируем отправляемые данные
            var requestData = new Request()
            {
                ModelId = "gpt-3.5-turbo",
                Messages = messages
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(requestData));
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = _httpClient.PostAsync("http://127.0.0.1:1337/v1/chat/completions", stringContent).GetAwaiter().GetResult())
            {

                // если произошла ошибка, выводим сообщение об ошибке на консоль
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
                    return "";
                }
                // получаем данные ответа
                var responseDataString = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<ResponseData>(responseDataString);

                var choices = responseData?.choices ?? new List<Choice>();
                if (choices.Count == 0)
                {
                    Console.WriteLine("No choices were returned by the API");
                    return "";
                }
                var choice = choices[0];
                var responseMessage = choice.message;
                // добавляем полученное сообщение в список сообщений
                messages.Add(responseMessage);
                var responseText = responseMessage.Content.Trim();

                return responseText;
            }
        }
    }
}
