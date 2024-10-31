using System;
using System.Net.Http.Headers;


namespace Program
{
    class Prog
    {

        static void DoSomething(int seconds, string mgs, ConsoleColor color)
        {
            lock (Console.Out) {
                Console.ForegroundColor = color;
                Console.WriteLine($"{mgs,10} ... Start");
                Console.ResetColor();
            }



            for (int i = 1; i <= seconds; i++)
            {
                lock (Console.Out) {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{mgs,10} {i,2}");
                    Console.ResetColor();
                }

                Thread.Sleep(1000);
            }

            lock (Console.Out) {
                Console.ForegroundColor = color;
                Console.WriteLine($"{mgs,10} ... End");
                Console.ResetColor();
            }

        }

        static async Task Task2()
        {
            Task t2 = new Task(() =>
            {
                DoSomething(8, "T2", ConsoleColor.Green);
            });
            t2.Start();
            await t2;
            Console.WriteLine("T2 da hoan thanh!");
        }

        static async Task Task3()
        {
            Task t3 = new Task(
                (object ob) =>
                {
                    string tentacvu = (string)ob;
                    DoSomething(4, tentacvu, ConsoleColor.Yellow);
                }, "T3"
                );
            t3.Start();
            await t3 ;

            Console.WriteLine("T3 da hoan thanh!");
        }
        static void Main(string[] args)
        {
            // Synchronous

            //DoSomething(5, "T1", ConsoleColor.Red);


            //t3.Wait();
            Task t2 = Task2();
            Task t3 = Task3();
            Task<string> t4 = Task4();
            Task<string> t5 = Task5();

            DoSomething(5, "T1", ConsoleColor.Red);
            Task.WaitAll(t2, t3, t4, t5);

            var kq4 = t4.Result;
            var kq5 = t5.Result;


            Console.WriteLine(kq4);
            Console.WriteLine(kq5);

            Console.WriteLine("Press any key");  
            Console.ReadKey();
        }   

        static async Task<string> Task4()
        {
            Task<string> t4 = new Task<string>(() =>
            {
                DoSomething(6, "T4", ConsoleColor.Magenta);
                return "Return from T4";
            });
            
            t4.Start();
            var kq = await t4;
            Console.WriteLine("T4 hoan thanh");
            return kq;
        }

        static async Task<string> Task5()
        {
            Task<string> t5 = new Task<string>
            (
                (object ob) =>
                {

                    string s = (string)ob;
                    DoSomething(6, s, ConsoleColor.Blue);
                    return $"Return from {s}";
                }, "T5"
            );

            t5.Start();
            var kq = await t5;
            Console.WriteLine("T5 hoan thanh");
            return kq;
        }


    }
}
