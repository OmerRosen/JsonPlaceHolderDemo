using System.Text;
using System.Text.Json;
using Models;

namespace JsonPlaceHolderDemo{
    public class Program{
        static void Main(string[] args){

            var newPostRequest = new CreateNewPostRequest{
                Body = "Demo Body",
                Title = "Omer the happy Capybara",
                UserId = 1
            };

            string baseUri = "https://jsonplaceholder.typicode.com";
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);

            var jsonRequest = JsonSerializer.Serialize(newPostRequest);
            var requestContant = new StringContent(jsonRequest, encoding: Encoding.UTF8, mediaType: "application/json");

            var response = client.PostAsync(requestUri: "posts", content: requestContant).Result;

            if(response.IsSuccessStatusCode){
                var responseContent = response.Content.ReadAsStringAsync().Result;
                System.Console.WriteLine($"responseContent: {responseContent}.");
                var postResponse = JsonSerializer.Deserialize<CreateNewPostResponse>(responseContent);
            }
            else{
                System.Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}