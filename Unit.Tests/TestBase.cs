using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Unit.Tests;

public class TestBase
{
    private const string _jsonMediaType = "application/json";
    private const int _expectedMaxElapsedMilliseconds = 1000;
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7107") };

    public TestBase()
    {

    }

    protected async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string requestUri)
    {
        var request = new HttpRequestMessage(httpMethod, requestUri);
        request.Headers.Add("X-Api-Key", "042848e7-b3bb-4f89-90c1-1a4f56ae84df");
        return await _httpClient.SendAsync(request);
    }

    protected static StringContent GetJsonStringContent<T>(T model)
        => new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);

    protected static async Task AssertResponseWithContentAsync<T>(
        Stopwatch stopwatch,
        HttpResponseMessage response,
        System.Net.HttpStatusCode expectedStatusCode,
        T expectedContent)
    {
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);

        var actualContent = await JsonSerializer.DeserializeAsync<T?>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
        Assert.Equal(expectedContent, actualContent);
    }

    private static void AssertCommonResponseParts(Stopwatch stopwatch,
        HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
    }

    protected void Dispose()
    {
        _httpClient.DeleteAsync("/state").GetAwaiter().GetResult();
    }
}
