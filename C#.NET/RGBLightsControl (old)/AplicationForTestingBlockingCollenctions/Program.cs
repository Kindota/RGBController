using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AplicationForTestingBlockingCollenctions
{
    class Program
    {
        public static BlockingCollection<string> buffer;
         
        static void Main(string[] args)
        {
            //IProducerConsumerCollection<string> tmp = new ConcurrentQueue<string>(); 
            buffer = new BlockingCollection<string>();
            buffer.Add("Penis");
            Thread otherThread = new Thread(new ThreadStart(readFromQueue));
            otherThread.Start();
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                buffer.Add(input);
            }
            otherThread.Abort();
            otherThread.Join();
        }

        public static void readFromQueue()
        {
            Console.WriteLine("Starting other thread");
            while (true)
            {
                string message;
                Console.WriteLine($"From queue {buffer.Take()}");
            }
        }
    }
}
