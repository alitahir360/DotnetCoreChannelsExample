using System.Threading.Channels;
using System.Threading.Tasks;

namespace DotnetCoreChannelsExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var channel = Channel.CreateBounded<string>(10);

            var producer1 = new Producer(channel.Writer, 1);
            var producer2 = new Producer(channel.Writer, 2);
            var consumer1 = new Consumer(channel.Reader, 1);
            var consumer2 = new Consumer(channel.Reader, 2);
            var consumer3 = new Consumer(channel.Reader, 3);

            var consumerTask1 = consumer1.ConsumeData();
            var consumerTask2 = consumer2.ConsumeData();
            var consumerTask3 = consumer3.ConsumeData();

            var producerTask1 = producer1.BeginProducing(1, 10);
            var producerTask2 = producer2.BeginProducing(11, 20);

            Task.WaitAll(Task.WhenAll(producerTask1, producerTask2).ContinueWith(_ => channel.Writer.Complete()));

            Task.WaitAll(consumerTask1, consumerTask2, consumerTask3);
        }
    }
}