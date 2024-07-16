using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Utils
{
    internal static class RequestLimiter
    {
        public static async Task<T[]> Call<T>(Func<Task<T>>[] funcs, int maxRequestPerSecond)
        {
            var result = new T[funcs.Length];
            if (funcs.Length < maxRequestPerSecond)
            {
                for (int i = 0; i < funcs.Length; i++)
                {
                    result[i] = await funcs[i]();
                }
                return result;
            }

            return await ExecuteByParts(funcs, maxRequestPerSecond);
        }

        private static async Task<T[]> ExecuteByParts<T>(Func<Task<T>>[] funcs, int maxRequestPerSecond)
        {
            var result = new T[funcs.Length];

            int executed = 0;
            while (executed < funcs.Length)
            {
                var tasks = new Task<T>[maxRequestPerSecond];
                var timer = Task.Delay(1000);
                for (int i = 0; i < maxRequestPerSecond && executed + i < funcs.Length; i++)
                {
                    tasks[i] = funcs[executed + i]();
                }
                var execution = Task.WhenAll(tasks);
                await Task.WhenAll(Task.WhenAll(tasks), timer);

                for (int i = 0; i < maxRequestPerSecond; i++)
                {
                    result[i + executed] = tasks[i].Result;
                }
                executed += maxRequestPerSecond;
            }
            return result;
        }
    }
}
