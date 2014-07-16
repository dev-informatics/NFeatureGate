using System.Collections.Generic;

namespace NFeatureGate.Contracts.Storage
{
    public interface INFeatureStorageAdapter
    {
        IEnumerable<NFeatureBranch> GetBranches();

        IEnumerable<NFeature> GetFeatures();

        void AddBranch(NFeatureBranch branch);

        void AddFeature(NFeature feature);

        NFeatureBranch GetFeatureBranch(string name);

        NFeature GetFeature(string name);

        void ClearStorage();
    }
}
