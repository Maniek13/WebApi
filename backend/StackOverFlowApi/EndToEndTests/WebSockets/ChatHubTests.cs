using EndToEndTests.ApplicationFactory;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;

namespace EndToEndTests.Endpoints;
public partial class ApplicationFactoryTests : IClassFixture<WebApiWebAplicationFactory>
{

    [Fact]
    public async Task WebSockets_ShouldSendMessage()
    {
        var connection = new HubConnectionBuilder()
        .WithUrl($"{_httpClient.BaseAddress}chat", options =>
        {
            options.HttpMessageHandlerFactory = _ => _testServer.CreateHandler(); 
        })
        .Build();

        string message = string.Empty; ;

        connection.On<string>("ReceiveMessage", msg =>
        {
            message = msg;
        });

        await connection.StartAsync();

        await connection.InvokeAsync("SendMessage", "User", "Message", "");

        await Task.Delay(1000);

        await connection.StopAsync();

        message.Should().Be("User: Message");
    }
}