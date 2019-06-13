using System;
using System.IO;

namespace wg_net_redis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(File.ReadAllText("Intro.txt"));

        }
    }
}
