using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysString
{
    internal class OnCropGatherReplaceOnGrowableGatheredString : CodeFixStrategyString
    {
        public override string Fix(string text)
        {
            return text.Replace("OnCropGather", "OnGrowableGathered");
        }
    }
}
