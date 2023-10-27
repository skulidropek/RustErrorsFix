﻿using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class ResearchTableGetBlueprintTemplateStringGetLine : CodeFixStrategyStringGetLine
    {
        public ResearchTableGetBlueprintTemplateStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            return code.Replace(errorLineString, errorLineString.Replace("ResearchTable.GetBlueprintTemplate()", "Workbench.GetBlueprintTemplate()"));
        }
    }
}
