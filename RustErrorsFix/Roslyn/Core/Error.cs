using Roslyn_test.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roslyn_test.Core
{
    class Error
    {
        public int Line;
        public int Symbol;
        public string Text;

        public List<AbstractFactory> AbstractFactorys = new List<AbstractFactory>();
    }
}
