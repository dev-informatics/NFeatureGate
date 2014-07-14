using NFeatureGate.Contracts.Storage;
using System.Linq;

namespace NFeatureGate
{
    public class NFeatureContext 
    {
        public NFeatureBranch ActiveBranch { get; set; }

        private readonly INFeatureStorageAdapter _storageAdapter;

        public NFeatureContext(INFeatureStorageAdapter storageAdapter)
        {
            _storageAdapter = storageAdapter;
            ActiveBranch = _storageAdapter.GetBranches().FirstOrDefault(n => n.IsActive);
        }
    }
}
