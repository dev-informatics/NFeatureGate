using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate.Storage
{
    public interface IStorageAdapter
    {
        IEnumerable<NFeatureBranch> FeatureBranches { get; }

        IEnumerable<NFeature> Features { get; }
    }
}
