using System.Collections.Generic;
using ServeUp.System;

namespace ServeUp.Models
{
    public class Identity
    {
        public int Id { get; set; }

        public List<Claim> Claims { get; set; }
    }
}