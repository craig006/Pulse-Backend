using System;
using System.Threading.Tasks;
using ServeUp.System;

namespace Tests.Models
{
    public class MyMiddleware : IMiddleWare
    {
        public static int RunCount = 0;

        public bool DidRun = false;
        public int RanAtCount = 0;

        public Func<Task> Next { get; set; }

        public async Task Invoke()
        {
            RunCount++;
            RanAtCount = RunCount;
            DidRun = true;
            await Next();
        }
    }
}