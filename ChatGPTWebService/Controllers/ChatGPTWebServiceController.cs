using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatGPTWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTWebServiceController : ControllerBase
    {
        // POST api/<ChatGPTWebServiceController>
        [HttpPost]
        public string Post([FromBody] string prompt)
        {
            // Replace YOUR_API_KEY with your actual API key
            string apiKey = "<YOUR_API_KEY>";

            // Set the model for the request
            string model = "text-davinci-003";

            // Send the request to the OpenAI API
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            HttpResponseMessage response = client.PostAsync($"https://api.openai.com/v1/completions",
                new StringContent($"{{\"model\": \"{model}\", \"prompt\": \"{prompt}\", \"max_tokens\": 512}}",
                System.Text.Encoding.UTF8, "application/json")).Result;

            // Read the response and extract the answer
            string json = response.Content.ReadAsStringAsync().Result;
            JObject result = JObject.Parse(json);
            string answer = result["choices"][0]["text"].ToString();

            // Return the answer
            return answer.TrimStart('\n', '\r');
        }
    }
}
