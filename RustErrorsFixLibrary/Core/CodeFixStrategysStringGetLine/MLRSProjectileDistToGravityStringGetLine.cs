using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class MLRSProjectileDistToGravityStringGetLine : CodeFixStrategyStringGetLine
    {
        public MLRSProjectileDistToGravityStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            if(!code.Contains("float ProjectileDistToGravity"))
                code = Regex.Replace(code, @"([\d\w]+\s[\d\w]+\(.+\)[\s\n]*{)", "$1\nfloat ProjectileDistToGravity(float xSKULIDROPEK, float ySKULIDROPEK, float θSKULIDROPEK, float vSKULIDROPEK)\r\n            {\r\n                float numSKULIDROPEK = θSKULIDROPEK * ((float)System.Math.PI / 180f);\r\n                float num2SKULIDROPEK = (vSKULIDROPEK * vSKULIDROPEK * xSKULIDROPEK * Mathf.Sin(2f * numSKULIDROPEK) - 2f * vSKULIDROPEK * vSKULIDROPEK * ySKULIDROPEK * Mathf.Cos(numSKULIDROPEK) * Mathf.Cos(numSKULIDROPEK)) / (xSKULIDROPEK * xSKULIDROPEK);\r\n                if (float.IsNaN(num2SKULIDROPEK) || num2SKULIDROPEK < 0.01f)\r\n                {\r\n                    num2SKULIDROPEK = 0f - Physics.gravity.y;\r\n                }\r\n\r\n                return num2SKULIDROPEK;\r\n            }");

            return code.Replace(errorLineString, errorLineString.Replace("MLRS.", ""));
        }
    }
}
