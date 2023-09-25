using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Test
    {
        IServiceProvider _serviceProvider;

        public Test(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Test1()
        {
            Console.WriteLine("Ворк");
            //_serviceProvider.GetService<IMyService>().DoSomething();
        }
    }
}
