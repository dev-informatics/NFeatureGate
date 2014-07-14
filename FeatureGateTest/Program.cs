using MemoryStorageAdapter;
using NFeatureGate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureGateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NFeatureContext ctx = new NFeatureContext(new InMemoryStorageAdapter());
            if(ctx.ActiveBranch.Features["DemoFeature"].IsEnabled())
            {
                Console.WriteLine("The DemoFeature is Enabled");
            }
            else
            {
                Console.WriteLine("The DemoFeature is not Enabled");
            }
            Console.WriteLine(string.Format("The current active branch is {0}", ctx.ActiveBranch.Name));
            Console.ReadKey();
        }
    }
}
