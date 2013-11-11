﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Services
{
    public interface ILogger
    {
        void Log(string message, params object[] args);
    }
}
