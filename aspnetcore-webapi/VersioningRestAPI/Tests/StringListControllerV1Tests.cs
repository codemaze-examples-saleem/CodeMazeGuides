using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests;

public class StringListControllerV1Tests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public StringListControllerV1Tests(WebApplicationFactory<Program> factory)
    {
        const string serviceUrl = "https://localhost:7114/";
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri(serviceUrl);
    }

    [Fact]
    public async Task GivenDefaultCall_WhenNoVersion_ThenReturnStringStartingWithB()
    {
        var json = await _httpClient.GetStringAsync("/api/StringList");
        var strings = JArray.Parse(json);

        Assert.Equal(2, strings.Count);
        Assert.StartsWith("S", (string?)strings[0]);
        Assert.StartsWith("S", (string?)strings[1]);
    }

    [Fact]
    public async Task GivenQueryString_WhenCalledV1_ThenReturnStringStartingWithB()
    {
        var json = await _httpClient.GetStringAsync("/api/StringList?api-version=1.0");
        var strings = JArray.Parse(json);

        Assert.Equal(2, strings.Count);
        Assert.StartsWith("B", (string?)strings[0]);
        Assert.StartsWith("B", (string?)strings[1]);
    }
}