using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NFeatureGate
{
    public class NFeatureBranch
    {
        public NFeatureBranch()
        {
            Features = new Dictionary<string, NFeature>();
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IDictionary<string, NFeature> Features { get; set; } 
    }
}
