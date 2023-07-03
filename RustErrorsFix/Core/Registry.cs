using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Core
{
    internal static class Registry
    {
        const string RegistryName = "RustErrorsFix";

        public static void AddValue(string valueName, string value)
        {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegistryName);
            if (registry == null)
                return;

            registry.SetValue(valueName, value);
            registry.Close();
        }

        public static void SetValue(string valueName, string value)
        {
            var registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryName, true);
            if (registry == null)
                return;

            registry.SetValue(valueName, value);
            registry.Close();
        }

        public static string GetValue(string valueName)
        {
            var registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryName);

            if (registry == null)
                return "";

            var value = registry?.GetValue(valueName);

            if (value == null)
                return "";

            registry.Close();

            return (string)value;
        }
    }
}
