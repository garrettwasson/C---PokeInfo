using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokeInfo 
{
    public class WebRequest
    {
        // Second paramenter is function that returns dictionary of string keys to object values.
        // If an API returned an array as its top level collection the parameter type would be "Action".
        public static async Task GetPokemonDataAsync(int PokeId, Action<Dictionary<string, object>> Callback)
        {
           // Establish temporary HttpClient connection.
           using (var Client = new HttpClient())
           {
               try
               {
                   Client.BaseAddress = new Uri($"http://pokeapi.co/api/v2/pokemon/{PokeId}");
                   HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
                   Response.EnsureSuccessStatusCode(); // Throw error if not successful.
                   string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.

                   // Parse result into JSON and convert to a dictionary
                   // DeserializeObject will only parse the top level object
                   Dictionary<string, object> JsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(StringResponse);

                   // Execute callback by passing in the reponse 
                   Callback(JsonResponse);
               }
               catch (HttpRequestException e)
               {
                   // If something went wrong, display error.
                   Console.WriteLine($"Request exception: {e.Message}");
               }
           }
        }
    }
}