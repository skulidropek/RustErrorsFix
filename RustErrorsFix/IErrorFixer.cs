using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix
{
    internal interface IErrorFixer
    {
        public string Fix(string plugin);
    }
}
