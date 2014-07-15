using MemoryStorageAdapter;
using NFeatureGate;
using System;

namespace FeatureGateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NFeatureContext ctx = new NFeatureContext(new InMemoryStorageAdapter());
            if(ctx.ActiveBranch.BranchFeatureStates["Demo"].IsActive())
            {
                Console.WriteLine("The DemoFeature is Active");
            }
            else
            {
                Console.WriteLine("The DemoFeature is not Active");
            }
            Console.WriteLine(string.Format("The current active branch is {0}", ctx.ActiveBranch.Name));
            Console.ReadKey();
        }
    }
}
