using System.Collections.Generic;
using System.Linq;

namespace NFeatureGate
{
    public class NBranchFeatureStateCollection : IEnumerable<NBranchFeatureState>
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
            get { return _states.FirstOrDefault(n => n.Feature.Name.Trim().ToLower().Equals(name.Trim().ToLower())); }
        }

        public IEnumerator<NBranchFeatureState> GetEnumerator()
        {
            return _states.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _states.GetEnumerator();
        }
    }
}
