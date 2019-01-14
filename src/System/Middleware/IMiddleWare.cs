using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServeUp.System;

namespace ServeUp.System
{
    public interface IMiddleWare
    {
        Func<Task> Next { set; }

        Task Invoke();
    }

}

