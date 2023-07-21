using Superplay.Models;
using System;
using System.Linq;

namespace Superplay.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.EnsureCreated();

                // Look for any players.
                if (context.Players.Any())
                {

                    foreach (var device in context.Devices)
                    {
                        Console.WriteLine($"deviceId={device.Id} playerId={device.PlayerId}");
                    }
                    return;   // DB has been seeded
                }
                var random = new Random(12345);

                var numberOfUsers = random.Next(5, 50);

                for (int i = 0; i < numberOfUsers; i++)
                {
                    var player = new Player
                    {
                        Id = Guid.NewGuid(),
                        Coins = random.Next(0, 10000),
                        Rolls = random.Next(0, 1000)
                    };

                    context.Players.Add(player);

                    var numberOfDevicesForPlayer = random.Next(5, 50);

                    for (int j = 0; j < numberOfDevicesForPlayer; j++)
                    {
                        var coinFlip = random.NextDouble() < 0.5;

                        var device = new Device
                        {
                            Id = Guid.NewGuid(),
                            PlayerId = player.Id,
                            Type = coinFlip ? DeviceType.iOS : DeviceType.Android
                        };

                        context.Devices.Add(device);
                    }

                }
                context.SaveChanges();
            }
        }
    }
}