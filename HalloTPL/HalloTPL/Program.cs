using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            int z = 12;
            Console.WriteLine("Hello World! " + dateTime.ToString("d") + " v" + z.ToString("000"));
            Console.WriteLine(String.Format("Hello World! {0:d} v{1:000}", dateTime, z));
            //string-interpolation
            Console.WriteLine($"Hello World! {dateTime:d} v{z:000}");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 1000000, i => Console.WriteLine(i));

            Task t1 = new Task(() =>
            {
                //im hintergrund Task (Thread) ausgeführt
                Console.WriteLine("T1 gestartet");
                throw new FileNotFoundException();

                Thread.Sleep(3000);
                Console.WriteLine("T1 fertig");

            });

            //immer
            t1.ContinueWith(t =>
            {
                Console.WriteLine("T1 Continue");
            });

            //nur bei exceptiony    
            t1.ContinueWith(t =>
            {
                Console.WriteLine($"T1 Error: {t.Exception.InnerException.Message}");
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);

            //nur wenn keine Exeption auftrat
            t1.ContinueWith(t =>
            {
                Console.WriteLine("T1 OK");
            }, CancellationToken.None, TaskContinuationOptions.NotOnFaulted, TaskScheduler.Default);

            Task<long> t2 = new Task<long>(() =>
            {

                Console.WriteLine("T2 gestartet");
                Thread.Sleep(6000);
                Console.WriteLine("T2 fertig");
                return 948357943857;
            });

            t1.Start();
            t2.Start();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
