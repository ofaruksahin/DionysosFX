﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    public interface IHttpContextHandler
    {
        Task HandleContextAsync(IHttpContextImpl context);
    }
}
