using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class TimedForEach
{
    public static async Task ForEachWithDelay<T>(this ICollection<T> items, Func<T, Task> action, double interval)
    {
        using(var timer = new System.Timers.Timer(interval))
        {
            var task = new Task(() => { });
            int remaining = items.Count;
            var queue = new ConcurrentQueue<T>(items);

            timer.Elapsed += async (sender, args) =>
            {
                T item;

                if (queue.TryDequeue(out item))
                {
                    try
                    {
                        await action(item);
                    }
                    finally
                    {
                        remaining -= 1;

                        if (remaining == 0)
                        {
                            task.Start();
                        }
                    }
                }
            };

            timer.Start();
            await task;
        }
    }
}
