using RustErrorsFix.Core.Abstract;
using RustErrorsFix.Legasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Core.Factory
{
    internal class LegasyPluginFixerAbstractFactory : PluginFixerAbstractFactory
    {
        private List<IErrorFixer> _errors = new List<IErrorFixer>()
        {
            new NetValueError(),
            new EntityListError(),
            new NetWrite(),
            new UpgradeError(),
            new CCTV_RCError(),
            new ItemContainerError(),
            new UsingError(),
            new UnexpectedTokenError(),
            new WaterLevelError(),
            new NpcFixError(),
        };

        public override string FixPlugin(string pluginContent)
        {
            foreach (var fixer in _errors)
            {
                pluginContent = fixer.Fix(pluginContent);
            }

            return pluginContent;
        }
    }
}
