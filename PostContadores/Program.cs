using PostContadores.EventSources;
using System.Threading;

namespace PostContadores
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var exampleEventSource = new ExampleEventSource();

            for (var i = 0; i < 3000; i++)
            {
                exampleEventSource.Increment();
                Thread.Sleep(100);
            }
        }
    }
}
