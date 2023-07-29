using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Core.Abstract
{
    internal abstract class PluginFixerAbstractFactory
    {
        public abstract string FixPlugin(string pluginContent);
    }
}
