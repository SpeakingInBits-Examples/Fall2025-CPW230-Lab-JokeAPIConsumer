using System.Diagnostics;
using JokeAPIConsumer.Models;
using Microsoft.AspNetCore.Mvc;

namespace JokeAPIConsumer.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // API Url to get any joke excluding sensitive categories
            string jokeApiUrl = "https://v2.jokeapi.dev/joke/Any?blacklistFlags=nsfw,religious,political,racist,sexist,explicit";

            // Create an instance of HttpClient to make the API request
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(jokeApiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to a Joke object
                Joke joke = System.Text.Json.JsonSerializer.Deserialize<Joke>(jsonResponse);

                // Pass the joke to the view
                return View(joke);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
