using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roslyn_test.Interface
{
    internal interface IPluginErrorFix
    {
        IEnumerable<string> Errors { get; }

        void Fix();
    }
}
