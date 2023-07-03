using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Core
{
    public abstract class Sigleton<T> where T : Sigleton<T>
    {
        public static T Instance;

        public Sigleton()
        {
            Instance = (T)this;
        }
    }
}
