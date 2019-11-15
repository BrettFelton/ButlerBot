using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using ButlerBot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ButlerBot
{
       class Program
    {
        static async Task Main(string[] args)
            => await new StreamMusicBotClient().InitializeAsync();
    }
}
