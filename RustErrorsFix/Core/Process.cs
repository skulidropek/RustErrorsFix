using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDiagnosticsProcess = System.Diagnostics.Process;

namespace RustErrorsFix.Core
{
    internal static class Process
    {
        public static void Start(string process)
        {
            SystemDiagnosticsProcess.Start(new ProcessStartInfo(process) { UseShellExecute = true });
        }
    }
}
