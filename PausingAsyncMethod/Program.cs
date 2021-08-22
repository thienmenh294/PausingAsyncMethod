﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PausingAsyncMethod
{
    class Program
    {
        static void Main()
        {
            var pts = new PauseTokenSource();
            Task.Run(() =>
            {
                while (true)
                {
                    Console.ReadLine();
                    pts.IsPaused = !pts.IsPaused;
                }
            });
            SomeMethodAsync(pts.Token).Wait();
        }

        public static async Task SomeMethodAsync(PauseToken pause)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(100);
                await pause.WaitWhilePausedAsync();
            }
        }
    }
}
