﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.FW.Bpm.Engine.Dmn.Feel.Impl
{
    public interface IFeelEngineFactory
    {
        IFeelEngine CreateInstance();
    }
}
