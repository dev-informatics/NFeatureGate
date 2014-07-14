using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NFeatureGate
{
    public class NFeatureBranch
    {
        public NFeatureBranch()
        {
            BranchFeatureStates = new NBranchFeatureStateCollection();
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public NBranchFeatureStateCollection BranchFeatureStates { get; set; } 
    }
}
