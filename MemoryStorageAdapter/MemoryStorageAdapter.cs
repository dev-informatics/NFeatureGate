using NFeatureGate;
using NFeatureGate.Contracts.Storage;
using System.Collections.Generic;
using System.Linq;

namespace MemoryStorageAdapter
{
    public class InMemoryStorageAdapter : INFeatureStorageAdapter
    {
        private List<NFeatureBranch> _featureBranches;
        private List<NFeature> _baseGates;

        public InMemoryStorageAdapter()
        {
            _baseGates = new List<NFeature>()
            {
                new NFeature()
                {
                    Name = "Demo"
                },
                new NFeature()
                {
                    Name = "Test"
                }
            };

            _featureBranches = new List<NFeatureBranch>()
            {
                new NFeatureBranch()
                {
                    IsActive = true,
                    Name = "Development",
                    Features = _baseGates.ToDictionary(n => n.Name, n => n)
                }
            };
        }

        public IEnumerable<NFeatureBranch> GetBranches()
        {
            return _featureBranches;
        }

        public IEnumerable<NFeature> GetFeatures()
        {
            return _baseGates;
        }

        public void AddBranch(NFeatureBranch branch)
        {
            _featureBranches.Add(branch);
        }

        public void AddFeature(NFeature feature)
        {
            _baseGates.Add(feature);
        }
    }
}
