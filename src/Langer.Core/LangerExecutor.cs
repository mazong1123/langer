using System;
using System.Collections.Generic;

namespace Langer.Core
{
    public abstract class LangerExecutor
    {
        public abstract string Execute(string binaryFilePath, List<string> parameters);
    }
}