using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DotnetCoreChannelsExample
{
    public class Producer
    {
        private readonly ChannelWriter<string> _writer;
        private readonly int _writerId;
        private readonly Random _random = new Random();
        
        public Producer(ChannelWriter<string> channelWriter, int writerId)
        {
            _writer = channelWriter;
            _writerId = writerId;
        }

        public async Task BeginProducing(int start, int end)
        {
            for (var i = start; i < end; i++)
            {
                await Task.Delay((_random.Next(1, 3) * 1000));
                Console.WriteLine($"Producer {_writerId} > {i}");
                await _writer.WriteAsync($"{i}");
            }
        }
    }
}