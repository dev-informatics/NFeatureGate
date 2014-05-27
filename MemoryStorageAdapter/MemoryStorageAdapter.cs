using NFeatureGate;
using NFeatureGate.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryStorageAdapter
{
    public class InMemoryStorageAdapter : IStorageAdapter
    {
        private List<NFeatureBranch> _featureBranches;
        private List<NFeature> _baseGates;

        public InMemoryStorageAdapter()
        {
            _baseGates = new List<NFeature>()
            {
                new NFeature()
                {
                    IsEnabled = true,
                    Name = "DemoFeature"
                }
            };

            List<NFeatureState> states = new List<NFeatureState>()
            {
                new NFeatureState()
                {
                    Feature = _baseGates[0],
                    State = true
                }
            };

            _featureBranches = new List<NFeatureBranch>()
            {
                new NFeatureBranch()
                {
                    FeatureCollection = new NFeatureCollection(states),
                    Name = "Development",
                    IsActive = true
                }
            };
        }

        public IEnumerable<NFeatureBranch> FeatureBranches
        {
            get { return _featureBranches; }
        }

        public IEnumerable<NFeature> Features
        {
            get { return _baseGates; }
        }
    }
}
