using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DotnetCoreChannelsExample
{
    public class Consumer
    {
        private readonly ChannelReader<string> _reader;
        private readonly int _channelNumber;
        private readonly Random _random = new Random();

        public Consumer(ChannelReader<string> channelReader, int channelNumber)
        {
            _reader = channelReader;
            _channelNumber = channelNumber;
        }

        public async Task ConsumeData()
        {
            while (await _reader.WaitToReadAsync())
            {
                if (_reader.TryRead(out var msg))
                {
                    await Task.Delay((_random.Next(1, 3) * 1000));
                    Console.WriteLine($"Consumer {_channelNumber} < {msg}");
                }
            }
        }
    }
}