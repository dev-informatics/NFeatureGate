using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate
{
    public class NBranchFeatureStateCollection
    {
        private readonly List<NBranchFeatureState> _states = new List<NBranchFeatureState>();

        public NBranchFeatureStateCollection()
        {
            
        }

        public NBranchFeatureStateCollection(IEnumerable<NBranchFeatureState> branchFeatureStates)
        {
            _states = branchFeatureStates.ToList();
        }

        public NBranchFeatureState this[string name]
        {
            get { return _states.FirstOrDefault(n => n.Feature.Name == name); }
        }
    }
}
