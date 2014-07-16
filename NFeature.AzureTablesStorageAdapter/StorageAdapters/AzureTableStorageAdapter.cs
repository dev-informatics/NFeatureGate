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
        private readonly CloudTableClient _cloudTableClient;
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

        public IEnumerable<NFeatureBranch> GetBranches()
        {
            var branchList = new List<NFeatureBranch>();

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

        public IEnumerable<NFeature> GetFeatures()
        {
            return _featuresTable.CreateQuery<FeatureEntity>().Select(n => new NFeature()
            {
                Name = n.Name
            }).ToList();
        }

        public void AddBranch(NFeatureBranch branch)
        {
            var insertOp = TableOperation.Insert(new BranchEntity()
            {
                PartitionKey = Guid.Empty.ToString(),
                RowKey = Guid.NewGuid().ToString(),
                IsEnabled = branch.IsActive,
                Name = branch.Name
            });

            if (branch.BranchFeatureStates.Count() != _featuresTable.CreateQuery<FeatureEntity>().ToList().Count())
                throw new Exception("Branch Feature State count must match the number of Features in the data-store.");

            branch.BranchFeatureStates.ToList().ForEach(bfs => CreateBranchFeatureState(new BranchFeatureEntity()
            {
                PartitionKey = Guid.Empty.ToString(),
                Feature = bfs.Feature.Name,
                Branch = branch.Name,
                IsActive = bfs.IsEnabled
            }));
            _branchesTable.Execute(insertOp);
        }

        public void AddFeature(NFeature feature)
        {
            var insertOp = TableOperation.Insert(new FeatureEntity()
            {
                PartitionKey = Guid.Empty.ToString(),
                RowKey = Guid.NewGuid().ToString(),
                Name = feature.Name
            });
            _featuresTable.Execute(insertOp);
        }

        private void CreateBranchFeatureState(BranchFeatureEntity entity)
        {
            entity.PartitionKey = Guid.Empty.ToString();
            entity.RowKey = Guid.NewGuid().ToString();
            var insertOp = TableOperation.Insert(entity);
            _branchFeaturesTable.Execute(insertOp);
        }
        
        public NFeatureBranch GetFeatureBranch(string name)
        {
            var branch = _branchesTable.CreateQuery<BranchEntity>().FirstOrDefault(n => n.Name.Trim().ToLower().Equals(name.Trim().ToLower()));

            return new NFeatureBranch()
            {
                BranchFeatureStates = new NBranchFeatureStateCollection(GetBranchFeatures(branch)),
                IsActive = branch.IsEnabled,
                Name = branch.Name
            };
        }

        public NFeature GetFeature(string name)
        {
            var feature =
                _featuresTable.CreateQuery<FeatureEntity>()
                    .FirstOrDefault(n => n.Name.Trim().ToLower().Equals(name.Trim().ToLower()));

            return new NFeature()
            {
                Name = feature.Name
            };
        }

        public void ClearStorage()
        {
            _featuresTable.CreateQuery<FeatureEntity>().ToList().ForEach(f =>
            {
                var delOp = TableOperation.Delete(f);
                _featuresTable.Execute(delOp);
            });
            _branchFeaturesTable.CreateQuery<BranchFeatureEntity>().ToList().ForEach(bf =>
            {
                var delOp = TableOperation.Delete(bf);
                _branchFeaturesTable.Execute(delOp);
            });
            _branchesTable.CreateQuery<BranchEntity>().ToList().ForEach(b =>
            {
                var delOp = TableOperation.Delete(b);
                _branchesTable.Execute(delOp);
            });
            
        }
    }
}
