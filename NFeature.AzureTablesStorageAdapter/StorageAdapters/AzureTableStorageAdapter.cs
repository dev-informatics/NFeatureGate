using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NFeatureGate.AzureTablesStorageAdapter.Models;
using NFeatureGate.Contracts.Storage;

namespace NFeatureGate.StorageAdapters
{
    public class AzureTableStorageAdapter : INFeatureStorageAdapter
    {
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly CloudTable _featuresTable;
        private readonly CloudTable _branchesTable;
        private readonly CloudTable _branchFeaturesTable;

        public AzureTableStorageAdapter(string connectionString)
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = _cloudStorageAccount.CreateCloudTableClient();
            _featuresTable = cloudTableClient.GetTableReference("Features");
            _featuresTable.CreateIfNotExists();
            _branchesTable = cloudTableClient.GetTableReference("Branches");
            _branchesTable.CreateIfNotExists();
            _branchFeaturesTable = cloudTableClient.GetTableReference("BranchFeatures");
            _branchFeaturesTable.CreateIfNotExists();
        }

        public IEnumerable<NFeatureGate.NFeatureBranch> GetBranches()
        {
            List<NFeatureBranch> branchList = new List<NFeatureBranch>();

            _branchesTable.CreateQuery<BranchEntity>().ToList().ForEach(branch => branchList.Add(new NFeatureBranch()
            {
                BranchFeatureStates = new NBranchFeatureStateCollection(GetBranchFeatures(branch)),
                IsActive = branch.IsEnabled,
                Name = branch.Name
            }));

            return branchList;
        }

        private IEnumerable<NBranchFeatureState> GetBranchFeatures(BranchEntity branch)
        {
            var featuresStates = new List<NBranchFeatureState>();

            _branchFeaturesTable.CreateQuery<BranchFeatureEntity>().Where(n => n.Branch == branch.Name).ToList().ForEach(
                bfe => featuresStates.Add(new NBranchFeatureState()
                {
                    IsEnabled = bfe.IsActive,
                    Feature = _featuresTable.CreateQuery<FeatureEntity>().Where(n => n.Name == bfe.Feature).Select(fe => new NFeature()
                    {
                        Name = fe.Name
                    }).FirstOrDefault()
                }));

            return featuresStates;
        }

        public System.Collections.Generic.IEnumerable<NFeatureGate.NFeature> GetFeatures()
        {
            return _featuresTable.CreateQuery<FeatureEntity>().Select(n => new NFeature()
            {
                Name = n.Name
            }).ToList();
        }

        public void AddBranch(NFeatureGate.NFeatureBranch branch)
        {
            var insertOp = TableOperation.InsertOrReplace(new BranchEntity()
            {
                IsEnabled = branch.IsActive,
                Name = branch.Name
            });

            if (branch.BranchFeatureStates.Count() != _featuresTable.CreateQuery<FeatureEntity>().Count())
                throw new Exception("Branch Feature State count must match the number of Features in the data-store.");

            branch.BranchFeatureStates.ToList().ForEach(bfs => CreateBranchFeatureState(new BranchFeatureEntity()
            {
                Branch = bfs.Feature.Name,
                IsActive = bfs.IsEnabled
            }));
            _branchesTable.Execute(insertOp);
        }

        public void AddFeature(NFeatureGate.NFeature feature)
        {
            var insertOp = TableOperation.InsertOrReplace(new FeatureEntity()
            {
                Name = feature.Name
            });
            _featuresTable.Execute(insertOp);
        }

        private void CreateBranchFeatureState(BranchFeatureEntity entity)
        {
            var insertOp = TableOperation.InsertOrReplace(entity);
            _branchFeaturesTable.Execute(insertOp);
        }
    }
}
