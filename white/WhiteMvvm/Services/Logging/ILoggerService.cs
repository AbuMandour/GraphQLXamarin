﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMvvm.Services.Logging
{
    public interface ILoggerService
    {
        Task LogException(Exception exception);
    }
}
