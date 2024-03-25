

HttpClient client = new HttpClient();

//HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, @"https://google.com/");
HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, @"https://randomuser.me/api/");


HttpResponseMessage response = await client.SendAsync(request);

Console.WriteLine($"Status:{response.StatusCode}");

foreach(var header in response.Headers)
{
    Console.Write($"{header.Key}: ");
    foreach(string val in header.Value)
        Console.Write($"{val}\t");

    Console.WriteLine();
}


Console.WriteLine("\n\n");

string? payload = await response.Content.ReadAsStringAsync();
Console.WriteLine(payload);