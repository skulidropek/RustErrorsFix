using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysGPTStringGetLine
{
    internal class TestGPTStringGetLine : CodeFixStrategyGPTStringGetLine
    {
        public TestGPTStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, string error)
        {
            var text = OpenAIAPI.GetCompletion($@"
Твоя роль: Разработчик плагинов для игры Rust используя модификацию Oxide C#

В плагине есть ошибка {error}.
Ошибка в данной строчке {errorLine}

Сам код: {code}
").GetAwaiter().GetResult();


            return "";
        }
    }
}
