using NFeatureGate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFeatureGate
{
    public class NBranchFeatureStateCollection : IEnumerable<NBranchFeatureState>
    {
        private readonly IDictionary<string, NBranchFeatureState> _states = new Dictionary<string, NBranchFeatureState>(StringComparer.OrdinalIgnoreCase);

        public NBranchFeatureStateCollection()
        {
            
        }

        public NBranchFeatureStateCollection(IEnumerable<NBranchFeatureState> branchFeatureStates)
        {
            branchFeatureStates.ToList().ForEach(s =>
            {
                s.Feature.NameChanged += UpdateFeatureName;
            });
        }

        public NBranchFeatureState this[string name]
        {
            get { return _states[name.Trim()]; }
        }

        public IEnumerator<NBranchFeatureState> GetEnumerator()
        {
            return _states.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _states.Values.GetEnumerator();
        }

        private void UpdateFeatureName(NFeature sender, NFeatureChangedEventArgs args)
        {
            var state = _states[args.Name];
            if (state == null)
                throw new InvalidOperationException("State Collection received event for non-existant feature");

            _states.Remove(args.Name);
            _states.Add(args.Name, state);
        }
    }
}
