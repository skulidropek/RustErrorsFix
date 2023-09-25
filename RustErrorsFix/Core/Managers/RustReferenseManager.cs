using RustErrorsFix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Roslyn.Managers
{
    internal static class RustReferenseManager
    {
        public static string Path
        {
            get => Registry.GetValue("RustReferensePath") ?? ""; 
            set 
            {
                if (string.IsNullOrEmpty(Registry.GetValue("RustReferensePath")))
                {
                    Registry.AddValue("RustReferensePath", value);
                    return;
                }

                Registry.SetValue("RustReferensePath", value);
            }
        }
    }
}
