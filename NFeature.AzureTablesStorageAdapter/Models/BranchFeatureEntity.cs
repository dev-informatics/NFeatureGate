using Microsoft.WindowsAzure.Storage.Table;

namespace NFeatureGate.AzureTablesStorageAdapter.Models
{
    public class BranchFeatureEntity : TableEntity
    {
        public string Branch { get; set; }
        public string Feature { get; set; }
        public bool IsActive { get; set; }
    }
}
