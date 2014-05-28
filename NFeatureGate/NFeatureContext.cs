using NFeatureGate.Storage;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate
{
    public class NFeatureContext 
    {
        public NFeatureBranch ActiveBranch { get; set; }

        private readonly IStorageAdapter _storageAdapter;

        public NFeatureContext(IStorageAdapter storageAdapter)
        {
            _storageAdapter = storageAdapter;
            ActiveBranch = _storageAdapter.FeatureBranches.FirstOrDefault(n => n.IsActive);
            ActiveBranch.CurrentContext = this;
        }
    }
}
