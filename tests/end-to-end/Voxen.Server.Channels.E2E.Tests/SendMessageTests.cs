using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Voxen.Server.Authentication.Endpoints.Login;
using Voxen.Server.Channels.Endpoints.CreateChannel;
using Voxen.Server.Channels.Endpoints.SendMessage;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Channels.E2E.Tests;

public class SendMessageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public SendMessageTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder => { });
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task SendMessageEndpoint_CorrectlyTriggersSignalR()
    {
        var ct = TestContext.Current.CancellationToken;

        // -----------------------------
        // 1. Log in
        // -----------------------------
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "Password123!"
        };
        var loginResponse = await _client.PostAsJsonAsync("/users/login", loginRequest, ct);
        loginResponse.EnsureSuccessStatusCode();

        var loginContent = await loginResponse.Content.ReadAsStringAsync(ct);
        var loginResult = JsonSerializer.Deserialize<LoginResponse>(loginContent, JsonOptions);
        Assert.NotNull(loginResult?.AccessToken);

        // -----------------------------
        // 2. Create Text Channel
        // -----------------------------
        var createChannelRequest = new CreateChannelRequest
        {
            Name = "SendMessageTest",
            Type = ChannelType.Text
        };
        var createChannelHttpRequest = new HttpRequestMessage(HttpMethod.Post, "/channels")
        {
            Content = JsonContent.Create(createChannelRequest)
        };
        createChannelHttpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);

        var createChannelResponse = await _client.SendAsync(createChannelHttpRequest, ct);
        createChannelResponse.EnsureSuccessStatusCode();

        var createChannelContent = await createChannelResponse.Content.ReadAsStringAsync(ct);
        var createChannelResult = JsonSerializer.Deserialize<CreateChannelResponse>(createChannelContent, JsonOptions);
        Assert.NotNull(createChannelResult?.Id);

        // -----------------------------
        // 3. Setup in-memory SignalR connection
        // -----------------------------
        var handler = _factory.Server.CreateHandler();
        var baseUri = _factory.Server.BaseAddress;

        var connection = new HubConnectionBuilder()
            .WithUrl(new Uri(baseUri, "/channels/connect"), options =>
            {
                options.HttpMessageHandlerFactory = _ => handler;
                options.AccessTokenProvider = () => Task.FromResult(loginResult.AccessToken);
            })
            .WithAutomaticReconnect()
            .Build();

        var tcs = new TaskCompletionSource<SendMessageResponse>();
        connection.On<SendMessageResponse>("ReceiveMessage", msg =>
        {
            tcs.TrySetResult(msg);
        });

        await connection.StartAsync(ct);
        await connection.InvokeAsync("JoinChannel", createChannelResult.Id, ct);

        // -----------------------------
        // 4. Send a message to the channel
        // -----------------------------
        var sendMessageRequest = new SendMessageRequest
        {
            Content = "Hello world!"
        };
        var sendMessageHttpRequest = new HttpRequestMessage(HttpMethod.Post,
            $"/channels/{createChannelResult.Id}/messages")
        {
            Content = JsonContent.Create(sendMessageRequest)
        };
        sendMessageHttpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);

        var sendMessageResponse = await _client.SendAsync(sendMessageHttpRequest, ct);
        sendMessageResponse.EnsureSuccessStatusCode();

        // -----------------------------
        // 5. Wait for SignalR message (timeout in 5s)
        // -----------------------------
        var receivedMessage = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);
        Assert.Equal("Hello world!", receivedMessage.Content);

        // -----------------------------
        // 6. Sever connection
        // -----------------------------
        await connection.InvokeAsync("LeaveChannel", createChannelResult.Id, ct);
        await connection.StopAsync(ct);
    }
}
